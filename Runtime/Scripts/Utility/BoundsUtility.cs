using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Utility
{
    public static class BoundsUtility
    {
        
        
        public static void DrawBounds(this Bounds bounds ,Color color )
        {
           
            Vector3 center = bounds.center;
            Vector3 extents = bounds.extents;

            Vector3[] corners = new Vector3[8];

            corners[0] = center + new Vector3(-extents.x, extents.y, -extents.z);
            corners[1] = center + new Vector3(extents.x, extents.y, -extents.z);
            corners[2] = center + new Vector3(extents.x, extents.y, extents.z);
            corners[3] = center + new Vector3(-extents.x, extents.y, extents.z);

            corners[4] = center + new Vector3(-extents.x, -extents.y, -extents.z);
            corners[5] = center + new Vector3(extents.x, -extents.y, -extents.z);
            corners[6] = center + new Vector3(extents.x, -extents.y, extents.z);
            corners[7] = center + new Vector3(-extents.x, -extents.y, extents.z);
            Debug.DrawLine(corners[0], corners[1], color);
            Debug.DrawLine(corners[1], corners[2], color);
            Debug.DrawLine(corners[2], corners[3], color);
            Debug.DrawLine(corners[3], corners[0], color);

            Debug.DrawLine(corners[4], corners[5], color);
            Debug.DrawLine(corners[5], corners[6], color);
            Debug.DrawLine(corners[6], corners[7], color);
            Debug.DrawLine(corners[7], corners[4], color);

            Debug.DrawLine(corners[0], corners[4], color);
            Debug.DrawLine(corners[1], corners[5], color);
            Debug.DrawLine(corners[2], corners[6], color);
            Debug.DrawLine(corners[3], corners[7], color);
           
           
        }
    }
}