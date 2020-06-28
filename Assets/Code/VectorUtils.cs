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

    public static Vector2Int Normalize(this Vector2Int vector)
    {
        if (vector.sqrMagnitude <= 0)
        {
            return Vector2Int.zero;
        }

        if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
        {
            vector.x /= Mathf.Abs(vector.x);
            vector.y = 0;
        }
        else
        {
            vector.x = 0;
            vector.y /= Mathf.Abs(vector.y);
        }

        return vector;
    }
}
