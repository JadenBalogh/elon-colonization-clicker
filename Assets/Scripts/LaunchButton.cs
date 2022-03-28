using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchButton : MonoBehaviour
{
    [SerializeField] private float clickCost = 500000;
    [SerializeField] private Transform shipSpawn;
    [SerializeField] private ShipData shipData;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        GameManager.ResourceSystem.OnMoneyChanged.AddListener(UpdateClickable);
        UpdateClickable(GameManager.ResourceSystem.Money);
    }

    public void Launch()
    {
        if (GameManager.ResourceSystem.Money < clickCost) return;

        GameManager.ResourceSystem.AddMoney(-clickCost);

        Ship ship = Instantiate(GameManager.ShipSystem.ShipPrefab, shipSpawn.position, Quaternion.identity, GameManager.Canvas);
        ship.Launch(shipData);
    }

    private void UpdateClickable(float moneyAmount)
    {
        button.interactable = moneyAmount >= clickCost;
    }
}
