using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] int money;
    [SerializeField] int maxMoney = int.MaxValue;
    [SerializeField] TMP_Text moneyAmountText = null;

    public static MoneyManager Instance { get; private set; }

    public int Money
    {
        get => money;
        set
        {
            money = Mathf.Clamp(value, 0, maxMoney);
        }
    }

    void Awake()
    {
        Instance = this;
    }

    void LateUpdate()
    {
        moneyAmountText.text = $"{string.Format("{0:#,0}", money).Replace(',', ' ')} $";
    }
}
