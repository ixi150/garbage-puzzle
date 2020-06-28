using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorUtils
{
    public static Vector2Int Snap(this Vector3 v)
    {
        return new Vector2Int
        {
            x = Mathf.RoundToInt(v.x),
            y = Mathf.RoundToInt(v.y),
        };
    }
}
