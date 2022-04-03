using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceDisplay : MonoBehaviour
{
    private readonly string[] Suffixes = { "", "K", "M", "B", "T" };

    [SerializeField] private string prefix;
    [SerializeField] private ResourceProperty property;

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
        GameManager.ResourceSystem.AddPropertyListener(property, UpdateText);
    }

    protected virtual void UpdateText(float amount)
    {
        textbox.text = FormatText(amount, "F0");
    }

    protected virtual string FormatText(float amount, string format)
    {
        int k = 0;
        if (amount > 0)
        {
            k = (int)(Mathf.Log10(amount) / 3);
        }
        float dividor = Mathf.Pow(10, k * 3);
        return prefix + (amount / dividor).ToString(format) + Suffixes[k];
    }
}
