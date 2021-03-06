﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] Vector3 speed = default;

    void LateUpdate()
    {
        transform.Rotate(speed * Time.deltaTime);
    }
}
