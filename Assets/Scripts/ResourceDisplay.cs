using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceDisplay : MonoBehaviour
{
    private readonly string[] Suffixes = { "", "K", "M", "B", "T" };

    [SerializeField] private string prefix;
    [SerializeField] protected ResourceProperty property;
    [SerializeField] private bool isRounded = false;
    [SerializeField] private bool hasCap = false;
    [SerializeField] private ResourceProperty propertyCap;

    protected TextMeshProUGUI textbox;

    private void Awake()
    {
        textbox = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        AddResourceListener();
    }

    protected void AddResourceListener()
    {
        GameManager.ResourceSystem.AddPropertyListener(property, (prop) => UpdateText());
        if (hasCap)
        {
            GameManager.ResourceSystem.AddPropertyListener(propertyCap, (prop) => UpdateText());
        }
    }

    protected virtual void UpdateText()
    {
        string prop = FormatText(GameManager.ResourceSystem.GetProperty(property));
        string propCap = FormatText(GameManager.ResourceSystem.GetProperty(propertyCap));
        textbox.text = prop + (hasCap ? " / " + propCap : "");
    }

    protected virtual string FormatText(float amount)
    {
        int k = 0;
        if (amount > 0)
        {
            k = (int)(Mathf.Log10(amount) / 3);
        }
        float dividor = Mathf.Pow(10, k * 3);
        float roundedAmount = isRounded ? Mathf.FloorToInt(amount) : amount;
        string format = roundedAmount % dividor == 0 ? "F0" : "F1";
        return prefix + (roundedAmount / dividor).ToString(format) + Suffixes[k];
    }
}
