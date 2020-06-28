using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEdge : MonoBehaviour
{
    [SerializeField] EdgeCollider2D edge;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Awake()
    {
        var halfSize = spriteRenderer.size/2;
        var points = new Vector2[5];
        points[0] = new Vector2(-halfSize.x, halfSize.y);
        points[1] = new Vector2(halfSize.x, halfSize.y);
        points[2] = new Vector2(halfSize.x, -halfSize.y);
        points[3] = new Vector2(-halfSize.x, -halfSize.y);
        points[4] = points[0];
        edge.points = points;
    }
}
