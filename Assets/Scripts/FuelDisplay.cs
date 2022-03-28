using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuelDisplay : ResourceDisplay
{
    protected override void AddResourceListener()
    {
        GameManager.ResourceSystem.OnFuelChanged.AddListener(UpdateText);
    }
}
