using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private GridXZ<bool> grid;
    void Start() 
    {
        //grid = new GridXZ<bool>(10, 10, 1f, new Vector3(0,0,0));   
        grid = new GridXZ<bool>(10, 10, 1f, new Vector3(0,0,0));   


        //  grid = new GridXZ(2, 2, 5f, new Vector3(0,0,0));
        // Vector3 newOriginpos = grid.GetWorldPosition(1, 1);
        // grid2 = new GridXZ(10,10, 0.5f, newOriginpos);
    }

    void Update()
    {
        Vector3 mousePos;

        if(Input.GetMouseButton(0))
        {      
                  
            if(Utility.MouseUtility.GetMousePositonOn3DSpace(out mousePos))
            {
                grid.SetValue(mousePos, true);
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if(Utility.MouseUtility.GetMousePositonOn3DSpace(out mousePos))
            {
                print(grid.GetValue(mousePos));
            }
        }
    }
}