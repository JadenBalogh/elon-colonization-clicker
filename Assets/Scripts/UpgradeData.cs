using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "UpgradeData", order = 51)]
public class UpgradeData : ScriptableObject
{
    public float moneyRate = 0;
    public float peopleRate = 0;
    public float fuelRate = 0;
    public float foodRate = 0;
}
