using UnityEngine;
using System.Collections;
using TMPro;
using Unity.Mathematics;

[ExecuteAlways]
public class ItemDrop : MonoBehaviour
{
    [SerializeField] ItemType itemType = null;
    [SerializeField] bool isSpawner = false;
    [SerializeField] SpriteRenderer arrowRenderer = null;
    [SerializeField] SpriteRenderer itemRenderer = null;
    [SerializeField] TMP_Text itemName = null;
    [SerializeField] TMP_Text itemPrice = null;
    [SerializeField] LayerMask itemLayerMask = -1;
    [SerializeField] int buyPriceIncrease = 2;

    public bool IsSpawner
    {
        get => isSpawner;
        set
        {
            isSpawner = value;
            Refresh();
        }
    }

    public ItemType ItemType
    {
        get => itemType;
        set
        {
            itemType = value;
            Refresh();
        }
    }

    void OnEnable()
    {
        Refresh();
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

        if (isSpawner)
        {
            var pos = transform.position.Snap();
            if (itemType
                && WorldManager.CanMoveItem(pos, Vector2Int.right)
                && MoneyManager.Instance.Money >= itemType.Price + buyPriceIncrease)
            {
                var item = ItemFactory.Instance.SpawnItem(pos + Vector2Int.right, itemType);
                item.transform.position = transform.position;
                item.SetActive(false);
                item.SetActive(true, .5f);
                MoneyManager.Instance.ModifyMoney(-(itemType.Price + buyPriceIncrease), transform.position);
            }
        }
        else
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.right, 1, itemLayerMask);
            if (hit)
            {
                var item = hit.collider.GetComponent<Item>();
                if (item
                    && item.ItemType == itemType
                    && MoneyManager.Instance.Money >= -itemType.Price
                    && item.IsCloseToDesiredPosition
                    && !item.IsDragged)
                {
                    item.Position = transform.position.Snap();
                    item.SetActive(false);
                    Destroy(item.gameObject, 1);
                    MoneyManager.Instance.ModifyMoney(itemType.Price, transform.position);
                }
            }
        }
    }

    void Refresh()
    {
        arrowRenderer.flipX = !isSpawner;
        if (itemRenderer.enabled = itemType)
        {
            itemRenderer.color = itemType.Color;
            itemName.text = itemType.name;
            var price = itemType.Price;
            if (isSpawner)
            {
                price = -(price + buyPriceIncrease);
            }

            itemPrice.text = $"{(price >= 0 ? '+' : '-')}{math.abs(price)}$";
        }
        else
        {
            itemPrice.text = itemName.text = string.Empty;
        }

        name = $"{(isSpawner ? "Spawner" : "Destroyer")} {(itemType?.name ?? "empty")}";
    }
}
