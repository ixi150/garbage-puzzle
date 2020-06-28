using UnityEngine;
using System.Collections;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] Item itemPrefab = null;

    public static ItemFactory Instance { get; private set; }

    public Item SpawnItem(Vector2Int pos, ItemType type)
    {
        var item = Instantiate(itemPrefab, (Vector2)pos, Quaternion.identity);
        item.ItemType = type;
        return item;
    }

    void Awake()
    {
        Instance = this;
    }
}
