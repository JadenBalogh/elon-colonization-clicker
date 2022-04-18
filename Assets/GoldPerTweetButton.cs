using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldPerTweetButton : MonoBehaviour
{
    private readonly string[] Suffixes = { "", "K", "M", "B", "T" };

    [SerializeField] private TextMeshProUGUI upgradeTextbox;
    [SerializeField] private float upgradeAmount = 20000;
    [SerializeField] private float goldCost = 3000000;
    [SerializeField] private float goldCostMult = 1.2f;
    [SerializeField] private bool isOneTime = false;

    private Button button;
    private bool isPurchased = false;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        UpdateClickable(GameManager.ResourceSystem.GetProperty(ResourceProperty.Money));
        GameManager.ResourceSystem.AddPropertyListener(ResourceProperty.Money, UpdateClickable);
    }

    public void ApplyUpgrade()
    {
        if (isOneTime) isPurchased = true;

        GameManager.ResourceSystem.ChangeProperty(ResourceProperty.Money, -goldCost);
        goldCost *= goldCostMult;
        UpdateText();
        GameManager.TweetSystem.IncreaseGoldAverage(upgradeAmount);
    }

    private void UpdateText()
    {
        upgradeTextbox.text = isPurchased ? "-" : FormatText(goldCost);
    }

    private string FormatText(float amount)
    {
        int k = 0;
        if (amount > 0)
        {
            k = (int)(Mathf.Log10(amount) / 3);
        }
        float dividor = Mathf.Pow(10, k * 3);
        string format = amount % dividor == 0 ? "F0" : "F1";
        return (amount / dividor).ToString(format) + Suffixes[k];
    }

    private void UpdateClickable(float gold)
    {
        button.interactable = !isPurchased && gold >= goldCost;
    }
}
