using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// under construction
public class MapManager : MonoBehaviour
{
    private static MapManager _singleton;

    public static MapManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
            }
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(MapManager)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    
}