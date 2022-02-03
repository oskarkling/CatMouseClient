using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class MouseUtility
    {
        public static bool GetMousePositonOn3DSpace(out Vector3 mousePos)
        {
            mousePos = Vector3.one;

            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 1000 = range (maxdistance)
            if (Physics.Raycast(ray, out hitInfo, 1000))
            {
                mousePos = hitInfo.point;
                return true;
            }

            return false;
        }
    }

}