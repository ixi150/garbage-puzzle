using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyInfoManager : MonoBehaviour
{
    [SerializeField] TMP_Text textPrefab = null;
    [SerializeField] Color positiveColor = Color.green;
    [SerializeField] Color negativeColor = Color.red;
    [SerializeField] float duration = 1;
    [SerializeField] Vector3 offset = Vector2.up;

    new Camera camera;

    public void ShowInfo(int moneyChange, Vector3 position)
    {
        if (moneyChange == 0)
        {
            return;
        }

        var text = Instantiate(textPrefab, transform);
        text.color = moneyChange > 0 ? positiveColor : negativeColor;
        text.text = $"{(moneyChange > 0 ? "+" : "-")}{Mathf.Abs(moneyChange)}$";
        text.transform.position = camera.WorldToScreenPoint(position);
        Destroy(text.gameObject, duration);
        StartCoroutine(TextAnimation(text));
    }

    IEnumerator TextAnimation(TMP_Text text)
    {
        while (text != null)
        {
            float delta = Time.deltaTime / duration;
            var color = text.color;
            color.a -= delta;
            text.color = color;
            text.transform.position += offset * delta;
            yield return null;
        }
    }

    void Start()
    {
        camera = Camera.main;
    }
}
