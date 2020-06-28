using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] TMP_Text timeText = null;

    public static TimeManager Instance { get; private set; }

    float time;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        time = 0;
    }

    void LateUpdate()
    {
        time += Time.deltaTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = (int)time - minutes * 60;
        timeText.text = $"{minutes}:{seconds:00}";
    }
}
