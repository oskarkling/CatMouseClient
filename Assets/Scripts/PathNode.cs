using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GridXZ<PathNode> grid;
    private int x;
    private int z;

    // gCost = walking cost from the start node
    // hCost = "going straight" cost to reach end node
    // fCost = gCost + hCost

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;

    public PathNode cameFromNode;

    public PathNode(GridXZ<PathNode> _grid, int _x, int _z)
    {
        grid = _grid;
        x = _x;
        z = _z;
        isWalkable = true;
    }

    public override string ToString()
    {
        return x + "," + z;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public int GetX()
    {
        return x;
    }

    public int GetZ()
    {
        return z;
    }

    public void SetIsWalkable(bool _isWalkable)
    {
        isWalkable = _isWalkable;
    }
}
