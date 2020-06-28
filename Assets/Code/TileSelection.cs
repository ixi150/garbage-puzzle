using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelection : MonoBehaviour
{
    SpriteRenderer sr;
    new Camera camera;

    private void Awake()
    {
        TryGetComponent(out sr);
    }

    void Start()
    {
        camera = Camera.main;
    }

    private void LateUpdate()
    {
        //if (sr.enabled = Draggable.IsAnythingDragged)
        //{
        //    var pos = camera.ScreenToWorldPoint(Input.mousePosition);
        //    transform.position = (Vector2)pos.Snap();
        //}

        var pos = camera.ScreenToWorldPoint(Input.mousePosition).Snap();
        transform.position = (Vector2)pos;
        sr.enabled = WorldManager.IsThereTile(pos);
    }
}
