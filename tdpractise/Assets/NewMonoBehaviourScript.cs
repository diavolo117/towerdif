using UnityEngine;
using TMPro;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public static NewMonoBehaviourScript Instance { get; private set; }

    [SerializeField] private int resource = 50;
    [SerializeField] private TMP_Text moneyText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UpdateMoneyUI();
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = "Money: " + resource;
    }

    public void AddGold(int amount)
    {
        resource += amount;
        UpdateMoneyUI();
    }

    public bool Tryspendmoney(int cost)
    {
        if (resource > cost)
        {
            resource -= cost;
            return true;
        }
        return false;
    }

    public int GetMoney()
    {
        return resource;
    }
}