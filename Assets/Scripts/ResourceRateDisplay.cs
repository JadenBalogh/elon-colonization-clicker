using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceRateDisplay : ResourceDisplay
{
    protected override void UpdateText(float amount)
    {
        textbox.text = FormatText(amount, "F1");
    }

    protected override string FormatText(float amount, string format)
    {
        return "(" + base.FormatText(amount, format) + " / s)";
    }
}
