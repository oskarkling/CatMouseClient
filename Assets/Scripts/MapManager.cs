using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private GridXZ<GridInfo> grid;
    private Player player;

    void Start() 
    { 
        player = FindObjectOfType<Player>();
        grid = new GridXZ<GridInfo>(20, 10, 8f, new Vector3(0,0,0), (GridXZ<GridInfo> _grid, int _x, int _z) => new GridInfo(_grid, _x, _z));
    }

    void Update()
    {
        Vector3 mousePos;

        if(Input.GetMouseButton(0))
        {      
                  
            if(Utility.MouseUtility.GetMousePositonOn3DSpace(out mousePos))
            {
                GridInfo gridObj = grid.GetGridObject(mousePos);

                if(gridObj != null)
                {
                    gridObj.AddValue(5);
                }
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if(Utility.MouseUtility.GetMousePositonOn3DSpace(out mousePos))
            {
                //print(grid.GetGridObject(mousePos));
            }
        }

        CheckIfPlayerHasWalkedOverThisGridObj();
    }

    private void CheckIfPlayerHasWalkedOverThisGridObj()
    {
        GridInfo gridObj = grid.GetGridObject(player.transform.position);
        if(!gridObj.GetHaveWalkedOver())
        {
            gridObj.SetHaveWalkedOver(true);
        }
    }
}