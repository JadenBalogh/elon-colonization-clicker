using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweetButton : MonoBehaviour
{
    [SerializeField] private Transform tweetParent;
    [SerializeField] private Transform tweetSpawn;
    [SerializeField] private TweetResult tweetPrefab;
    [SerializeField] private int minMoneyPerClick = -20000;
    [SerializeField] private int maxMoneyPerClick = 100000;
    [SerializeField] private string[] tweetPrefixes;
    [SerializeField] private string[] tweetBodies;
    [SerializeField] private string[] tweetSuffixes;
    [SerializeField] private float clickCooldown = 1f;

    private WaitForSeconds clickWait;

    private Button button;

    private void Awake()
    {
        clickWait = new WaitForSeconds(clickCooldown);
        button = GetComponent<Button>();
    }

    public void GainMoney()
    {
        StartCoroutine(ClickCooldown());

        float money = Random.Range(minMoneyPerClick, maxMoneyPerClick);
        string prefix = tweetPrefixes[Random.Range(0, tweetPrefixes.Length)];
        string body = tweetBodies[Random.Range(0, tweetBodies.Length)];
        string suffix = tweetSuffixes[Random.Range(0, tweetSuffixes.Length)];
        string tweet = prefix + " " + body + " " + suffix;

        TweetResult result = Instantiate(tweetPrefab, tweetSpawn.position, Quaternion.identity, tweetParent);
        result.SetData(money, tweet);

        GameManager.ResourceSystem.ChangeProperty(ResourceProperty.Money, money);
    }

    private IEnumerator ClickCooldown()
    {
        button.interactable = false;
        yield return clickWait;
        button.interactable = true;
    }
}
