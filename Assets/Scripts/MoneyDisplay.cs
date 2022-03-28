using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : ResourceDisplay
{
    protected override void AddResourceListener()
    {
        GameManager.ResourceSystem.OnMoneyChanged.AddListener(UpdateText);
    }
}
