using System;
using System.Collections.Generic;
using UnityEngine;


public class GridInfo
{
    private int value;
    private bool isWalkable;
    private bool haveWalkedOver;
    private int x, z;
    private GridXZ<GridInfo> grid;
    public GridInfo(GridXZ<GridInfo> _grid, int _x, int _z)
    {
        grid = _grid;
        x = _x;
        z = _z;
    }

    public void AddValue(int addval)
    {
        value += addval;
        grid.TriggerGridObjectChanged(x, z);
    }

    public void ChangeIsWalkable(bool _change)
    {
        isWalkable = _change;
        grid.TriggerGridObjectChanged(x, z);
    }

    public void SetHaveWalkedOver(bool _change)
    {
        haveWalkedOver = _change;
        grid.TriggerGridObjectChanged(x, z);
    }
    
    public bool GetIsWalkable()
    {
        return isWalkable;
    }

    public bool GetHaveWalkedOver()
    {
        return haveWalkedOver;
    }

    public override string ToString()
    {
        return x.ToString() + ", " + z.ToString()+"\n" + haveWalkedOver.ToString();
    }
}