using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Building : MonoBehaviour, IMapElement
{
    [SerializeField] float duration = 1;
    [SerializeField] ItemType[] input;
    [SerializeField] ItemType[] output;

    List<ItemType> content = new List<ItemType>();
    float progress = -1f;
    Collider2D[] colliders;

    public List<ItemType> Content => content;
    public Collider2D[] Colliders => colliders;
    public bool IsInProgress => progress >= 0f;
    public float Progress => progress;

    public bool CanLoadResource(ItemType type)
    {
        var inputCount = input.Count(type.Equals);
        var contentCount = content.Count(type.Equals);
        return !IsInProgress && contentCount < inputCount;
    }

    void Awake()
    {
        colliders = GetComponentsInChildren<Collider2D>();
    }

    void OnEnable()
    {
        foreach (var col in colliders)
        {
            var pos = col.transform.position.Snap();
            WorldManager.AddElement(pos, this);
        }
    }

    void OnDisable()
    {
        foreach (var col in colliders)
        {
            WorldManager.RemoveElement(col.transform.position.Snap());
        }
    }

    void Update()
    {
        if (IsInProgress)
        {
            if (progress >= 1f)
            {
                //finish production
                if (CanEndProduction())
                {
                    if (progress >= 2f)
                    {
                        progress = -1f;
                    }
                    else
                    {
                        Content.AddRange(output);
                        progress = 2f;
                    }
                }
            }
            else
            {
                progress += Time.deltaTime / duration;
            }
        }
        else
        {
            if (CanStartProduction())
            {
                //start production
                progress = 0f;
                foreach (var itemType in input)
                {
                    Content.Remove(itemType);
                }
            }
        }
    }

    bool CanStartProduction()
    {
        foreach (var itemType in input)
        {
            var inputCount = input.Count(itemType.Equals);
            var contentCount = content.Count(itemType.Equals);
            if (contentCount < inputCount)
            {
                return false;
            }
        }

        return true;
    }

    bool CanEndProduction()
    {
        foreach (var itemType in output)
        {
            var contentCount = content.Count(itemType.Equals);
            if (contentCount > 0)
            {
                return false;
            }
        }

        return true;
    }
}
