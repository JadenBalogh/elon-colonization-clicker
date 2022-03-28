using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "ShipData", order = 51)]
public class ShipData : ScriptableObject
{
    public float peopleAmount = 0;
    public float fuelAmount = 0;
    public float foodAmount = 0;
}
