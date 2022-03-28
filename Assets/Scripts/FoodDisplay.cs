using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodDisplay : ResourceDisplay
{
    protected override void AddResourceListener()
    {
        GameManager.ResourceSystem.OnFoodChanged.AddListener(UpdateText);
    }
}
