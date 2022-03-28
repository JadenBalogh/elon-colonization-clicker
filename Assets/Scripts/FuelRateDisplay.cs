using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuelRateDisplay : ResourceRateDisplay
{
    protected override void AddResourceListener()
    {
        GameManager.ResourceSystem.OnFuelRateChanged.AddListener(UpdateText);
    }
}
