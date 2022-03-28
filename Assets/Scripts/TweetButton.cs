using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweetButton : MonoBehaviour
{
    [SerializeField] private int moneyPerClick = 10000;

    public void GainMoney()
    {
        GameManager.ResourceSystem.AddMoney(moneyPerClick);
    }
}
