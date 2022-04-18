using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TweetSystem : MonoBehaviour
{
    [SerializeField] private float goodTweetChanceStart = 0.01f;
    [SerializeField] private float goodTweetMultiplierStart = 4f;
    [SerializeField] private float tweetGoldAverageStart = 50000f;
    [SerializeField] private float tweetGoldRange = 50000f;

    public float GoodTweetChance { get; private set; }
    public float GoodTweetMultiplier { get; private set; }
    public float TweetGoldAverage { get; private set; }
    public UnityEvent<float> OnGoldAverageChanged { get; private set; }

    private void Awake()
    {
        OnGoldAverageChanged = new UnityEvent<float>();

        TweetGoldAverage = tweetGoldAverageStart;
        GoodTweetChance = goodTweetChanceStart;
        GoodTweetMultiplier = goodTweetMultiplierStart;
    }

    public void IncreaseGoldAverage(float delta)
    {
        TweetGoldAverage += delta;
        OnGoldAverageChanged.Invoke(TweetGoldAverage);
    }

    public float GetRandomMoney()
    {
        float goldAmount = TweetGoldAverage + Random.Range(-tweetGoldRange, tweetGoldRange);
        if (Random.value < GoodTweetChance)
        {
            goldAmount *= GoodTweetMultiplier;
        }
        return goldAmount;
    }
}
