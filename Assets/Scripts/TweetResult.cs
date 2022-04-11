using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TweetResult : MonoBehaviour
{
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
        moneyTextbox.text = (money >= 0 ? "+" : "-") + "$" + money;
        tweetTextbox.text = "\"" + text + "\"";
    }
}
