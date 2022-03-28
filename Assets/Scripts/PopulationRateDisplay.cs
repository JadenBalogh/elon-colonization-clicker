using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulationRateDisplay : ResourceRateDisplay
{
    protected override void AddResourceListener()
    {
        GameManager.ResourceSystem.OnPopulationRateChanged.AddListener(UpdateText);
    }
}
