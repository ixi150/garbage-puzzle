using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BuildingInProgress : MonoBehaviour
{
    [SerializeField] Renderer iconRenderer;
    [SerializeField] MonoBehaviour behaviour;

    Building building;

    void Awake()
    {
        building = GetComponentInParent<Building>();
        if (building == null)
        {
            Debug.LogError("BuildingInProgressComponents has no Building parent.", this);
        }
    }

    void LateUpdate()
    {
        behaviour.enabled = building.IsInProgress && building.Progress < 1f;
        iconRenderer.enabled = building.IsInProgress;
    }
}
