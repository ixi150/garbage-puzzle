using System;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour, IMapElement
{
    [SerializeField] ItemType itemType;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float closeDistance = 0.1f;

    Vector2Int position;
    Vector3 desiredPosition;
    Draggable draggable;

    public bool IsAtDesiredPosition => desiredPosition == transform.position;
    public bool IsCloseToDesiredPosition => (desiredPosition - transform.position).sqrMagnitude < closeDistance * closeDistance;

    public bool IsDragged => draggable?.IsDragged ?? false;

    public Vector2Int Position
    {
        get => position;
        set
        {
            RemoveFromMap();
            position = value;
            desiredPosition = (Vector2)position;
            WorldManager.AddElement(position, this);
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

    IEnumerator InvokeDelayed(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    public void SetActive(bool active, float delay = 0f)
    {
        if (delay > 0)
        {
            StartCoroutine(InvokeDelayed(() => SetActive(active), delay));
            return;
        }

        var draggable = GetComponent<Draggable>();
        if (draggable)
        {
            draggable.enabled = active;
        }

        var col = GetComponent<Collider2D>();
        if (col)
        {
            col.enabled = active;
        }
    }

    void Awake()
    {
        TryGetComponent(out draggable);
    }

    void OnEnable()
    {
        Position = transform.position.Snap();
        transform.position = (Vector2)position;
    }

    void OnDisable()
    {
        RemoveFromMap();
    }

    private void RemoveFromMap()
    {
        if (WorldManager.GetElement(position) == this as IMapElement)
        {
            WorldManager.RemoveElement(position);
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * moveSpeed);
    }

#if UNITY_EDITOR
    void LateUpdate()
    {
        Refresh();
    }
#endif

    void Refresh()
    {
        if (itemType == null)
        {
            return;
        }

        name = $"Item {ItemType.name}";
        if (spriteRenderer)
        {
            spriteRenderer.color = itemType.Color;
        }

        if (trailRenderer)
        {
            var gradient = trailRenderer.colorGradient;
            var gradientColors = gradient.colorKeys;
            for (int i = 0; i < gradientColors.Length; i++)
            {
                gradientColors[i].color = itemType.Color;
            }

            gradient.colorKeys = gradientColors;
            trailRenderer.colorGradient = gradient;
        }
    }
}
