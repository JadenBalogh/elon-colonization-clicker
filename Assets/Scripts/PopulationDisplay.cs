using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulationDisplay : ResourceDisplay
{
    protected override void AddResourceListener()
    {
        GameManager.ResourceSystem.OnPopulationChanged.AddListener(UpdateText);
    }
}
