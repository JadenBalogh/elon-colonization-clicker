using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceSystem : MonoBehaviour
{
    [SerializeField] private UpgradeData passiveGrowth;

    public float Money { get; private set; }
    public UnityEvent<float> OnMoneyChanged { get; private set; }

    public float Population { get; private set; }
    public UnityEvent<float> OnPopulationChanged { get; private set; }

    public float Fuel { get; private set; }
    public UnityEvent<float> OnFuelChanged { get; private set; }

    public float Food { get; private set; }
    public UnityEvent<float> OnFoodChanged { get; private set; }

    private float moneyRate = 0;
    public UnityEvent<float> OnMoneyRateChanged { get; private set; }

    private float populationRate = 0;
    public UnityEvent<float> OnPopulationRateChanged { get; private set; }

    private float fuelRate = 0;
    public UnityEvent<float> OnFuelRateChanged { get; private set; }

    private float foodRate = 0;
    public UnityEvent<float> OnFoodRateChanged { get; private set; }

    private void Awake()
    {
        OnMoneyChanged = new UnityEvent<float>();
        OnPopulationChanged = new UnityEvent<float>();
        OnFuelChanged = new UnityEvent<float>();
        OnFoodChanged = new UnityEvent<float>();

        OnMoneyRateChanged = new UnityEvent<float>();
        OnPopulationRateChanged = new UnityEvent<float>();
        OnFuelRateChanged = new UnityEvent<float>();
        OnFoodRateChanged = new UnityEvent<float>();
    }

    private void Start()
    {
        StartCoroutine(PassiveLoop());
    }

    private IEnumerator PassiveLoop()
    {
        WaitForSeconds waitOneSecond = new WaitForSeconds(1f);
        while (true)
        {
            AddMoney(moneyRate);
            AddPopulation(populationRate);
            AddFuel(fuelRate);
            AddFood(foodRate);

            yield return waitOneSecond;
        }
    }

    public void AddMoney(float money)
    {
        Money += money;
        OnMoneyChanged.Invoke(Money);
    }

    public void AddPopulation(float pop)
    {
        Population += pop;
        OnPopulationChanged.Invoke(Population);
        RefreshRates();
    }

    public void AddFuel(float fuel)
    {
        Fuel += fuel;
        OnFuelChanged.Invoke(Fuel);
    }

    public void AddFood(float food)
    {
        Food += food;
        OnFoodChanged.Invoke(Food);
    }

    private void RefreshRates()
    {
        float newMoneyRate = 0;
        float newPopulationRate = 0;
        float newFuelRate = 0;
        float newFoodRate = 0;

        if (Population >= 2)
        {
            newPopulationRate += passiveGrowth.peopleRate * Population;
        }
        newFoodRate += passiveGrowth.foodRate * Population;
        newFuelRate += passiveGrowth.fuelRate * Population;

        moneyRate = newMoneyRate;
        populationRate = newPopulationRate;
        fuelRate = newFuelRate;
        foodRate = newFoodRate;

        OnMoneyRateChanged.Invoke(moneyRate);
        OnPopulationRateChanged.Invoke(populationRate);
        OnFuelRateChanged.Invoke(fuelRate);
        OnFoodRateChanged.Invoke(foodRate);
    }
}
