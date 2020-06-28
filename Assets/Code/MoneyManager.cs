using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] int money;
    [SerializeField] int maxMoney = int.MaxValue;
    [SerializeField] TMP_Text moneyAmountText = null;
    [SerializeField] MoneyInfoManager moneyInfo;

    public static MoneyManager Instance { get; private set; }

    public int Money
    {
        get => money;
    }

    public void ModifyMoney(int amount, Vector3 position)
    {
        var newValue = Mathf.Clamp(money + amount, 0, maxMoney);
        moneyInfo.ShowInfo(newValue-money, position);
        money = newValue;
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
