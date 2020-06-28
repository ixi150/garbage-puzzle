using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemType : ScriptableObject
{
    [SerializeField] Color color = Color.white;
    [SerializeField] int price = 1;

    public Color Color => color;
    public int Price => price;
}
