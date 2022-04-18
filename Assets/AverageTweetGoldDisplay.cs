using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AverageTweetGoldDisplay : MonoBehaviour
{
    private readonly string[] Suffixes = { "", "K", "M", "B", "T" };

    protected TextMeshProUGUI textbox;

    private void Awake()
    {
        textbox = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.TweetSystem.OnGoldAverageChanged.AddListener((gold) => UpdateText());
        UpdateText();
    }

    protected virtual void UpdateText()
    {
        string prop = FormatText(GameManager.TweetSystem.TweetGoldAverage);
        textbox.text = "Avg Gold: " + prop;
    }

    protected virtual string FormatText(float amount)
    {
        int k = 0;
        if (amount > 0)
        {
            k = (int)(Mathf.Log10(amount) / 3);
        }
        float dividor = Mathf.Pow(10, k * 3);
        string format = amount % dividor == 0 ? "F0" : "F1";
        return (amount / dividor).ToString(format) + Suffixes[k];
    }
}
