using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI upgradeTextbox;
    [SerializeField] private string upgradeText;
    [SerializeField] private ResourceProperty property;
    [SerializeField] private UpgradeTier[] upgradeTiers;

    private int currTier = 0;

    private void Start()
    {
        Buy(); //TODO: implement buying
    }

    public void Buy()
    {
        GameManager.ResourceSystem.ChangeProperty(property, upgradeTiers[currTier].amount);
        UpdateText();
    }

    public void Upgrade()
    {
        GameManager.ResourceSystem.ChangeProperty(property, -upgradeTiers[currTier].amount);
        currTier++;
        GameManager.ResourceSystem.ChangeProperty(property, upgradeTiers[currTier].amount);
        UpdateText();
    }

    private void UpdateText()
    {
        upgradeTextbox.text = upgradeText + upgradeTiers[currTier].amount;
    }

    [System.Serializable]
    private struct UpgradeTier
    {
        public float amount;
        public float cost;
        public float populationReq;
    }
}
