using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingPathFinding : MonoBehaviour
{
    public GameObject wallPrefab;
    private PathFinding pathFinding;
    private void Start()
    {
        pathFinding = new PathFinding(10, 10);
    }

    private void Update() 
    { 
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(Utility.MouseUtility.GetMousePositonOn3DSpace(out Vector3 mousePos))
            {
                pathFinding.GetGrid().GetXZ(mousePos, out int x, out int z);

                List<PathNode> path = pathFinding.FindPath(0, 0, x, z);
                
                if(path != null)
                {
                    for(int i = 0; i < path.Count - 1; i++)
                    {
                        print(path[i].GetX().ToString() + ", " + path[i].GetZ().ToString());

                        int cellsize = (int)pathFinding.GetGrid().GetCellsize();

                        Debug.DrawLine(pathFinding.GetGrid().GetWorldPosition(path[i].GetX(), path[i].GetZ())  + new Vector3(cellsize / 2, 0, cellsize / 2), pathFinding.GetGrid().GetWorldPosition(path[i+1].GetX(), path[i+1].GetZ())  + new Vector3(cellsize / 2, 0, cellsize / 2), Color.red, 100f);
                    }
                }
            }
        }

                
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(Utility.MouseUtility.GetMousePositonOn3DSpace(out Vector3 mousePos))
            {
                pathFinding.GetGrid().GetXZ(mousePos, out int x, out int z);

                // PathNode node = pathFinding.GetNode(x, z);
                // node.isWalkable = false;
                // node.SetIsWalkable(false);
                //pathFinding.GetNode(x,z).SetIsWalkable(false);


                int cellsize = (int)pathFinding.GetGrid().GetCellsize();

                Vector3 middle = pathFinding.GetGrid().GetWorldPosition(x,z) + new Vector3(cellsize / 2, 0, cellsize / 2);

                pathFinding.GetNode(x, z).SetIsWalkable(!pathFinding.GetNode(x,z).isWalkable);

                Instantiate(wallPrefab, middle, Quaternion.identity);
            }
        }

    }
}
