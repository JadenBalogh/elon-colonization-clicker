using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceRateDisplay : ResourceDisplay
{
    protected override void UpdateText()
    {
        textbox.text = FormatText(GameManager.ResourceSystem.GetProperty(property));
    }

    protected override string FormatText(float amount)
    {
        return "(" + base.FormatText(amount) + " / day)";
    }
}
