using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldManager : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] Tile groundTile;
    [SerializeField] LayerMask blockingMask = -1;

    static WorldManager instance;
    readonly static RaycastHit2D[] nonAllocHits = new RaycastHit2D[100];
    readonly static Dictionary<Vector2Int, IMapElement> map = new Dictionary<Vector2Int, IMapElement>();

    public static IMapElement GetElement(Vector2Int pos)
    {
        return map.TryGetValue(pos, out var element) ? element : null;
    }

    public static bool HasElement(Vector2Int pos)
    {
        return map.ContainsKey(pos);
    }

    public static void AddElement(Vector2Int pos, IMapElement element)
    { 
        if (!HasElement(pos) && IsThereTile(pos))
        {
            map.Add(pos, element);
        }
    }

    public static void RemoveElement(Vector2Int pos)
    {
        map.Remove(pos);
    }

    public static bool CanMoveItem(Vector2Int position, Vector2Int direction, params Collider2D[] ignoreColliders)
    {
        var newPos = position + direction;
        if (HasElement(newPos) || !IsThereTile(newPos))
        {
            return false;
        }

        //var count = Physics2D.RaycastNonAlloc(position, direction, nonAllocHits, 1, instance.blockingMask);
        //for (int i = 0; i < count; i++)
        //{
        //    var col = nonAllocHits[i].collider;
        //    if (!ignoreColliders.Contains(col))
        //    {
        //        return false;
        //    }
        //}

        return true;
    }

    public static bool IsThereTile(Vector2Int pos)
    {
        return instance && instance.tilemap.GetTile((Vector3Int)pos) == instance.groundTile;
    }

    public static void AddTile(Vector2Int pos)
    {
        instance.tilemap.SetTile((Vector3Int)pos, instance.groundTile);
    }

    public static void ClearMap()
    {
        instance.tilemap.ClearAllTiles();
    }

    void Awake()
    {
        instance = this;
        map.Clear();
    }

}
