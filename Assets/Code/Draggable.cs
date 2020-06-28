using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;
    [SerializeField] Collider2D dragCollider;
    [SerializeField] float draggedSize = 1.2f;

    Item item;
    bool isDragged;
    new Camera camera;

    public static bool IsAnythingDragged { get; private set; }
    public bool IsDragged
    {
        get => isDragged;
        private set
        {
            if (IsAnythingDragged && value || isDragged == value)
            {
                return;
            }

            IsAnythingDragged = isDragged = value;
            if (trail)
            {
                trail.emitting = value;
            }
        }
    }

    public Vector2Int Position
    {
        get => item.Position;
        set => item.Position = value;
    }

    void Awake()
    {
        TryGetComponent(out item);
    }

    void OnDisable()
    {
        if (IsDragged)
        {
            IsDragged = false;
        }

        transform.localScale = Vector3.one;
    }

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        var scale = transform.localScale;
        var targetScale = isDragged ? draggedSize * Vector3.one : Vector3.one;
        scale = Vector3.Lerp(scale, targetScale, Time.deltaTime * 15);
        transform.localScale = scale;
        if (!isDragged)
        {
            return;
        }


        var newPos = camera.ScreenToWorldPoint(Input.mousePosition).Snap();
        var delta = newPos - Position;
        if (delta.sqrMagnitude > 0f)
        {
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                delta.x /= Mathf.Abs(delta.x);
                delta.y = 0;
            }
            else
            {
                delta.x = 0;
                delta.y /= Mathf.Abs(delta.y);
            }

            if (WorldManager.CanMoveItem(Position, delta, dragCollider))
            {
                newPos = Position + delta;
                Position = newPos;
                transform.position = (Vector2)newPos;
            }
        }


        if (!Input.GetMouseButton(0))
        {
            IsDragged = false;
        }
    }

    private void OnMouseDown()
    {
        if (!enabled)
        {
            return;
        }

        IsDragged = true;
    }
}
