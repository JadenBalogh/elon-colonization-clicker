using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Vector3 moveDir;
    private Vector3 targetPos;
    private float population = 0;
    private float parts = 0;
    private float food = 0;

    private void Start()
    {
        targetPos = (Vector2)Camera.main.ScreenToWorldPoint(GameManager.ShipSystem.ShipTarget);
        moveDir = (targetPos - transform.position).normalized;
    }

    private void Update()
    {
        transform.position += moveDir * GameManager.ShipSystem.ShipSpeed * Time.deltaTime;

        float distSqr = (targetPos - transform.position).sqrMagnitude;
        if (distSqr < 0.1f)
        {
            GameManager.ResourceSystem.ChangeProperty(ResourceProperty.Population, population);
            GameManager.ResourceSystem.ChangeProperty(ResourceProperty.Parts, parts);
            GameManager.ResourceSystem.ChangeProperty(ResourceProperty.Food, food);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.right, moveDir);
    }

    public void Launch(float population, float parts, float food)
    {
        this.population = population;
        this.parts = parts;
        this.food = food;
    }
}
