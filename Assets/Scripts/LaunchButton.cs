using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LaunchButton : MonoBehaviour
{
    [SerializeField] private float clickCost = 500000;
    [SerializeField] private Transform shipSpawn;
    [SerializeField] private float population = 0;
    [SerializeField] private float parts = 0;
    [SerializeField] private float food = 0;

    private TextMeshProUGUI textbox;
    private Button button;

    private void Awake()
    {
        textbox = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
    }

    private void Start()
    {
        GameManager.ResourceSystem.AddPropertyListener(ResourceProperty.Money, UpdateClickable);
        UpdateClickable(GameManager.ResourceSystem.GetProperty(ResourceProperty.Money));
        UpdateText();
    }

    public void Launch()
    {
        if (GameManager.ResourceSystem.GetProperty(ResourceProperty.Money) < clickCost) return;

        GameManager.ResourceSystem.ChangeProperty(ResourceProperty.Money, -clickCost);

        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(shipSpawn.position);
        Ship ship = Instantiate(GameManager.ShipSystem.ShipPrefab, spawnPos, Quaternion.identity);
        ship.Launch(population, parts, food);
    }

    private void UpdateClickable(float moneyAmount)
    {
        button.interactable = moneyAmount >= clickCost;
    }

    private void UpdateText()
    {
        float displayVal = 0;
        if (population > 0) displayVal = population;
        if (parts > 0) displayVal = parts;
        if (food > 0) displayVal = food;
        textbox.text = displayVal.ToString();
    }
}
