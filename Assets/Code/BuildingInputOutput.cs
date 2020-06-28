using UnityEngine;
using System.Collections;

[ExecuteAlways]
public class BuildingInputOutput : MonoBehaviour
{
    [SerializeField] ItemType itemType = null;
    [SerializeField] bool isInput = false;
    [SerializeField] LayerMask itemLayerMask;
    [SerializeField] SpriteRenderer arrowRenderer = null;
    [SerializeField] SpriteRenderer itemRenderer = null;

    Building building;

    void Awake()
    {
        building = GetComponentInParent<Building>();
        if (building == null)
        {
            Debug.LogError("BuildingInputOutput has no Building parent.", this);
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        Refresh();
        if (!Application.isPlaying)
        {
            return;
        }
#endif

        if (isInput)
        {
            var hit = Physics2D.Raycast(transform.position, transform.up, 1, itemLayerMask);
            if (hit && building.CanLoadResource(itemType))
            {
                var item = hit.collider.GetComponent<Item>();
                if (item && item.ItemType == itemType)
                {
                    item.Position = transform.position.Snap();
                    item.SetActive(false);
                    Destroy(item.gameObject, 1);
                    building.Content.Add(itemType);
                }
            }
        }
        else if (building.Content.Contains(itemType))
        {
            var pos = transform.position.Snap();
            Vector2Int direction = transform.up.Snap();
            if (itemType && WorldManager.CanMoveItem(pos, direction, building.Colliders))
            {
                var item = ItemFactory.Instance.SpawnItem(pos + direction, itemType);
                item.transform.position = (Vector2)pos;
                item.SetActive(false);
                item.SetActive(true, .5f);
                building.Content.Remove(itemType);
            }
        }
    }

    void Refresh()
    {
        if (itemType == null)
        {
            if (itemRenderer)
            {
                itemRenderer.enabled = false;
            }
            return;
        }

        if (arrowRenderer)
        {
            var scale = arrowRenderer.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (isInput ? -1 : 1);
            arrowRenderer.transform.localScale = scale;
        }

        if (itemRenderer && (itemRenderer.enabled = itemType))
        {
            itemRenderer.enabled = true;
            itemRenderer.color = itemType.Color;
        }

        name = $"Building{(isInput ? "Input" : "Output")} {itemType.name}";
    }
}
