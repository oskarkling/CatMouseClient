using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridXZ<TGridType>
{
    private int width;
    private int depth;
    private float cellSize;
    private TGridType[,] gridArray;
    private Vector3 originPosition;

    // debug arr
    private TextMesh[,] debugTextArr;

    public GridXZ(int _width, int _depth, float _cellSize, Vector3 _originPositon)
    {
        width = _width;
        depth = _depth;
        cellSize = _cellSize;
        originPosition = _originPositon;

        gridArray = new TGridType[width, depth];
        debugTextArr = new TextMesh[width, depth];

        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for(int z = 0; z < gridArray.GetLength(1); z++)
            {
                


                // ___ DEBUG PURPOSE ___

                Vector3 cellCenterPos = GetWorldPosition(x, z);
                cellCenterPos.x += cellSize * 0.5f;
                cellCenterPos.z += cellSize * 0.5f;
                debugTextArr[x, z] = Utility.TextUtils.CreateWorldTextXZ(gridArray[x, z].ToString(), null, cellCenterPos, 5, Color.white, TextAnchor.MiddleCenter);

                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);

                // ___ END OF DEBUG PURPOSE ___
            }
        }

        // __ DEBUG PURPOSE __
        Debug.DrawLine(GetWorldPosition(0, depth), GetWorldPosition(width, depth), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, depth), Color.white, 100f);
        // __ END OF DEBUG PURPOSE __
    }

    public Vector3 GetWorldPosition(int x, int z) 
    {
        return new Vector3(x, 0, z) * cellSize + originPosition;
    }

    public void GetXZ(Vector3 worldPos, out int x, out int z) 
    {
        x = Mathf.FloorToInt((worldPos - originPosition).x  / cellSize);
        z = Mathf.FloorToInt((worldPos - originPosition).z / cellSize);
    }

    public void SetValue(int x, int z, TGridType value)
    {
        if(x >= 0 && z >= 0 && x < width && z < depth)
        {
            gridArray[x,z] = value;
            debugTextArr[x, z].text = gridArray[x, z].ToString();
        }
    }

    public void SetValue(Vector3 worldPos, TGridType value)
    {
        int x, z;
        GetXZ(worldPos, out x, out z);
        SetValue(x, z, value);
    }

    public TGridType GetValue(int x, int z)
    {
        if(x >= 0 && z >= 0 && x < width && z < depth)
        {
            return gridArray[x, z];
        }
        else
        {
            return default(TGridType);
        }
    }

    public TGridType GetValue(Vector3 worldPos)
    {
        int x, z;
        GetXZ(worldPos, out x, out z);
        return GetValue(x, z);
    }
    
}
