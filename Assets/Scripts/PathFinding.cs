using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    // gCost = walking cost from the start node
    // hCost = "going straight" cost to reach end node
    // fCost = gCost + hCost
    private const int MoveStraightCost = 10;
    private const int MoveDiagonalCost = 14;
    private GridXZ<PathNode> grid;
    private List<PathNode> openList;

    // TODO
    // Implement generic Hashset<> on closedList to optimize performance
    // becouse we are checking if it contains the given node or not.
    private List<PathNode> closedList;

    public PathFinding(int width, int depth)
    {
        grid = new GridXZ<PathNode>(width, depth, 10f, Vector3.zero, (GridXZ<PathNode> g, int x, int z) => new PathNode(g, x, z));
    }

    public List<PathNode> FindPath(int startX, int startZ, int endX, int endZ)
    {
        PathNode startNode = grid.GetGridObject(startX, startZ);
        PathNode endNode = grid.GetGridObject(endX, endZ);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for(int x = 0; x < grid.GetWidth(); x++)
        {
            for(int z = 0; z < grid.GetDepth(); z++)
            {
                PathNode pathNode = grid.GetGridObject(x, z);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            } 
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        // Cycle
        while(openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            
            if(currentNode == endNode)
            {
                // Reached final node
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(var neighbourNode in GetNeighboursList(currentNode))
            {
                if(closedList.Contains(neighbourNode))
                {
                    continue;
                }

                if(!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);

                if(tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if(!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        // Out of nodes on the open list
        return null;
    }
    
    public PathNode GetNode(int x, int z)
    {
        return grid.GetGridObject(x, z);
    }

    public GridXZ<PathNode> GetGrid()
    {
        return grid;
    }

    public List<Vector3> FindPath(Vector3 startWorldPos, Vector3 endWorldPos)
    {
        grid.GetXZ(startWorldPos, out int startX, out int startZ);
        grid.GetXZ(endWorldPos, out int endX, out int endZ);

        List<PathNode> path = FindPath(startX, startZ, endX, endZ);

        if(path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vector3Path = new List<Vector3>();
            foreach(var pathNode in path)
            {
                vector3Path.Add(new Vector3(pathNode.GetX(), 0, pathNode.GetZ()) * grid.GetCellsize() + new Vector3(1, 0, 1) * grid.GetCellsize() * 0.5f);
            }
            
            return vector3Path;
        }
    }

    private List<PathNode> GetNeighboursList(PathNode currentNode)
    {
        List<PathNode> neighboursList = new List<PathNode>();

        if(currentNode.GetX() - 1 >= 0)
        {
            // Left
            neighboursList.Add(GetNode(currentNode.GetX() - 1, currentNode.GetZ()));

            //Left Down
            if(currentNode.GetZ() - 1 >= 0)
            {
                neighboursList.Add(GetNode(currentNode.GetX() - 1, currentNode.GetZ() - 1));
            }

            // Left Up
            if(currentNode.GetZ() + 1 < grid.GetDepth())
            {
                neighboursList.Add(GetNode(currentNode.GetX() - 1, currentNode.GetZ() +1));
            }
        }

        if(currentNode.GetX() + 1 < grid.GetWidth())
        {
            // Right
            neighboursList.Add(GetNode(currentNode.GetX() + 1, currentNode.GetZ()));

            // Right Down
            if(currentNode.GetZ() -1 >= 0)
            {
                neighboursList.Add(GetNode(currentNode.GetX() + 1, currentNode.GetZ() -1));
            }

            // Right Up
            if(currentNode.GetZ() + 1 < grid.GetDepth())
            {
                neighboursList.Add(GetNode(currentNode.GetX() + 1, currentNode.GetZ() + 1));
            }
        }

        // Down
        if(currentNode.GetZ() - 1 >= 0)
        {
            neighboursList.Add(GetNode(currentNode.GetX(), currentNode.GetZ() - 1));
        }

        // Up
        if(currentNode.GetZ() + 1 < grid.GetDepth())
        {
            neighboursList.Add(GetNode(currentNode.GetX(), currentNode.GetZ() + 1));
        }

        return neighboursList;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;

        while(currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }

        path.Reverse();

        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        // Mathf.Abs gives the absolute value, making negative values into positives.
        int xDistance = Mathf.Abs(a.GetX() - b.GetX());
        int zDistance = Mathf.Abs(a.GetZ() - b.GetZ());
        int remaining = Mathf.Abs(xDistance - zDistance);
        return MoveDiagonalCost * Mathf.Min(xDistance, zDistance) + MoveStraightCost * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> listOfPathNodes)
    {
        PathNode lowestFCostNode = listOfPathNodes[0];
        for(int i = 1; i < listOfPathNodes.Count; i++)
        {
            if(listOfPathNodes[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = listOfPathNodes[i];
            }
        }

        return lowestFCostNode;
    }

}
