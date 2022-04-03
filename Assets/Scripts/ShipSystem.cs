using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystem : MonoBehaviour
{
    [SerializeField] private Ship shipPrefab;
    [SerializeField] private Transform shipTarget;
    [SerializeField] private float baseShipSpeed = 2f;

    public Ship ShipPrefab { get => shipPrefab; }
    public Vector2 ShipTarget { get => shipTarget.position; }
    public float ShipSpeed { get => baseShipSpeed * shipSpeedMult; }

    private float shipSpeedMult = 1f;
}
