using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingDisplay : MonoBehaviour
{
    private readonly string[] Suffixes = { "", "K", "M", "B", "T" };

    [SerializeField] private TextMeshProUGUI titleTextbox;
    [SerializeField] private TextMeshProUGUI upgradeTextbox;
    [SerializeField] private TextMeshProUGUI upgradeButtonTextbox;
    [SerializeField] private TextMeshProUGUI nextTierTextbox;
    [SerializeField] private TextMeshProUGUI requirementsTextbox;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private string buildingName;
    [SerializeField] private string upgradeText;
    [SerializeField] private bool startPurchased = false;
    [SerializeField] private ResourceProperty property;
    [SerializeField] private ResourceProperty costProperty = ResourceProperty.Parts;
    [SerializeField] private UpgradeTier[] upgradeTiers;

    private int currTier = -1;

    private void Start()
    {
        GameManager.ResourceSystem.AddPropertyListener(costProperty, (parts) => UpdateClickable());
        GameManager.ResourceSystem.AddPropertyListener(ResourceProperty.Population, (pop) => UpdateClickable());
        if (startPurchased)
        {
            Buy();
        }
        UpdateComponents();
    }

    public void Buy()
    {
        currTier = 0;
        GameManager.ResourceSystem.ChangeProperty(costProperty, -upgradeTiers[currTier].cost);
        GameManager.ResourceSystem.RefreshProps();
        UpdateComponents();
    }

    public void Upgrade()
    {
        currTier++;
        GameManager.ResourceSystem.ChangeProperty(costProperty, -upgradeTiers[currTier].cost);
        GameManager.ResourceSystem.RefreshProps();
        UpdateComponents();
    }

    public void ApplyResources()
    {
        if (currTier >= 0)
        {
            GameManager.ResourceSystem.ChangeProperty(property, upgradeTiers[currTier].amount);
        }
    }

    private void UpdateComponents()
    {
        UpdateClickable();
        UpdateText();
    }

    private void UpdateClickable()
    {
        bool isCapped = currTier + 1 >= upgradeTiers.Length;
        if (!isCapped)
        {
            int costInt = Mathf.RoundToInt(GameManager.ResourceSystem.GetProperty(costProperty));
            int popInt = Mathf.RoundToInt(GameManager.ResourceSystem.GetProperty(ResourceProperty.Population));
            bool hasCost = costInt >= upgradeTiers[currTier + 1].cost;
            bool hasPop = popInt >= upgradeTiers[currTier + 1].populationReq;
            upgradeButton.interactable = hasCost && hasPop;
        }
        else
        {
            upgradeButton.interactable = false;
        }
    }

    private void UpdateText()
    {
        titleTextbox.color = currTier >= 0 ? Color.white : Color.grey;
        titleTextbox.text = buildingName + (currTier > 0 ? $" [Tier {currTier}]" : "");
        upgradeTextbox.text = upgradeText + (currTier >= 0 ? FormatText(upgradeTiers[currTier].amount) : "-");
        if (currTier + 1 < upgradeTiers.Length)
        {
            upgradeButtonTextbox.text = (currTier >= 0 ? "Upgrade " : "Buy ") + $"({FormatText(upgradeTiers[currTier + 1].cost)})";
            nextTierTextbox.text = (currTier >= 0 ? "Next Tier: " : "On Purchase: ") + FormatText(upgradeTiers[currTier + 1].amount);
            requirementsTextbox.text = "Min Population: " + FormatText(upgradeTiers[currTier + 1].populationReq);
        }
        else
        {
            nextTierTextbox.text = "MAX TIER";
            requirementsTextbox.text = "Min Population: -";
        }
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

    [System.Serializable]
    private struct UpgradeTier
    {
        public float amount;
        public float cost;
        public float populationReq;
    }
}
