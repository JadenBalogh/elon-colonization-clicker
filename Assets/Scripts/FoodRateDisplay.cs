using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodRateDisplay : ResourceRateDisplay
{
    protected override void AddResourceListener()
    {
        GameManager.ResourceSystem.OnFoodRateChanged.AddListener(UpdateText);
    }
}
