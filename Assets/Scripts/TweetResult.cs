using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TweetResult : MonoBehaviour
{
    private readonly string[] Suffixes = { "", "K", "M", "B", "T" };

    [SerializeField] private TextMeshProUGUI moneyTextbox;
    [SerializeField] private TextMeshProUGUI tweetTextbox;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float fadeTime = 2f;

    private CanvasGroup canvasGroup;
    private float alpha = 1;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        canvasGroup.alpha = alpha;
        alpha -= Time.deltaTime / fadeTime;
        if (alpha <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetData(float money, string text)
    {
        moneyTextbox.text = (money >= 0 ? "+" : "-") + "$" + FormatText(money);
        tweetTextbox.text = "\"" + text + "\"";
    }

    private string FormatText(float amount)
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
