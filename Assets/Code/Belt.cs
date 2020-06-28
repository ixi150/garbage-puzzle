using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    static readonly Dictionary<Vector2Int, Belt> belts = new Dictionary<Vector2Int, Belt>();

    public static Belt GetBelt(Vector2Int pos)
    {
        return belts.TryGetValue(pos, out var belt) ? belt : null;
    }

    void OnEnable()
    {
        belts.Add(transform.position.Snap(), this);
    }

    void OnDisable()
    {
        belts.Remove(transform.position.Snap());
    }

    void Update()
    {
        var pos = transform.position.Snap();
        var item = WorldManager.GetElement(pos) as Item;
        if (item && item.IsAtDesiredPosition && !item.IsDragged)
        {
            Vector2Int direction = transform.up.Snap();
            if (WorldManager.CanMoveItem(pos, direction))
            {
                item.Position = pos + direction;
            }
        }
    }
}
