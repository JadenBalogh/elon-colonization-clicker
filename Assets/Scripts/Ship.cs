using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Vector3 moveDir;
    private ShipData shipData;

    private void Start()
    {
        moveDir = (GameManager.ShipSystem.ShipTarget.position - transform.position).normalized;
    }

    private void Update()
    {
        transform.position += moveDir * GameManager.ShipSystem.ShipSpeed * Time.deltaTime;

        float distSqr = (GameManager.ShipSystem.ShipTarget.position - transform.position).sqrMagnitude;
        if (distSqr < 0.1f)
        {
            GameManager.ResourceSystem.AddFood(shipData.foodAmount);
            GameManager.ResourceSystem.AddFuel(shipData.fuelAmount);
            GameManager.ResourceSystem.AddPopulation(shipData.peopleAmount);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.right, moveDir);
    }

    public void Launch(ShipData shipData)
    {
        this.shipData = shipData;
    }
}
