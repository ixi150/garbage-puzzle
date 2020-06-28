using UnityEngine;
using System.Collections;

public class BeltManager : MonoBehaviour
{
    [SerializeField] Belt beltPrefab = null;

    new Camera camera;
    Vector2Int? clickedPosition;

    void Start()
    {
        camera = Camera.main;
    }

    void OnEnable()
    {
        clickedPosition = null;
    }

    void OnDisable()
    {
        clickedPosition = null;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            clickedPosition = null;
        }

        var pos = camera.ScreenToWorldPoint(Input.mousePosition).Snap();
        if (!WorldManager.IsThereTile(pos))
        {
            return;
        }

        if (Input.GetMouseButton(1)) //deleting
        {
            var belt = Belt.GetBelt(pos);
            if (belt)
            {
                Destroy(belt.gameObject);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickedPosition = pos;
            }

            if (clickedPosition.HasValue)
            {
                var delta = (pos - clickedPosition.Value).Normalize();
                if (delta.sqrMagnitude > 0)
                {
                    var building = WorldManager.GetElement(clickedPosition.Value) as Building;
                    if (!building && WorldManager.IsThereTile(clickedPosition.Value))
                    {
                        var belt = Belt.GetBelt(clickedPosition.Value);
                        if (belt == null)
                        {
                            belt = Instantiate(beltPrefab, (Vector2)clickedPosition.Value, Quaternion.identity, transform);
                        }

                        belt.transform.up = (Vector2)delta;
                        clickedPosition = clickedPosition.Value + delta;
                    }
                    else
                    {
                        clickedPosition = pos;
                    }
                }
            }
        }
    }
}
