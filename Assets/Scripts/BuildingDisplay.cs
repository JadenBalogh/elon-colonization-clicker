using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingDisplay : MonoBehaviour
{
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
    [SerializeField] private UpgradeTier[] upgradeTiers;

    private int currTier = -1;

    private void Start()
    {
        GameManager.ResourceSystem.AddPropertyListener(ResourceProperty.Parts, (parts) => UpdateClickable());
        GameManager.ResourceSystem.AddPropertyListener(ResourceProperty.Population, (pop) => UpdateClickable());
        if (startPurchased)
        {
            Buy(); //TODO: implement buying
        }
        UpdateComponents();
    }

    public void Buy()
    {
        currTier = 0;
        GameManager.ResourceSystem.RefreshProps();
        UpdateComponents();
    }

    public void Upgrade()
    {
        currTier++;
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
            int partsInt = Mathf.RoundToInt(GameManager.ResourceSystem.GetProperty(ResourceProperty.Parts));
            int popInt = Mathf.RoundToInt(GameManager.ResourceSystem.GetProperty(ResourceProperty.Population));
            bool hasParts = partsInt >= upgradeTiers[currTier + 1].cost;
            bool hasPop = popInt >= upgradeTiers[currTier + 1].populationReq;
            upgradeButton.interactable = hasParts && hasPop;
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
        upgradeTextbox.text = upgradeText + (currTier >= 0 ? upgradeTiers[currTier].amount : "-");
        if (currTier + 1 < upgradeTiers.Length)
        {
            upgradeButtonTextbox.text = (currTier >= 0 ? "Upgrade " : "Buy ") + $"({upgradeTiers[currTier + 1].cost})";
            nextTierTextbox.text = (currTier >= 0 ? "Next Tier: " : "On Purchase: ") + upgradeTiers[currTier + 1].amount;
            requirementsTextbox.text = "Min Population: " + upgradeTiers[currTier + 1].populationReq;
        }
        else
        {
            nextTierTextbox.text = "MAX TIER";
            requirementsTextbox.text = "Min Population: -";
        }
    }

    [System.Serializable]
    private struct UpgradeTier
    {
        public float amount;
        public float cost;
        public float populationReq;
    }
}
