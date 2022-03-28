using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyRateDisplay : ResourceRateDisplay
{
    protected override void AddResourceListener()
    {
        GameManager.ResourceSystem.OnMoneyRateChanged.AddListener(UpdateText);
    }
}
