using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceSystem : MonoBehaviour
{
    [SerializeField] private UpgradeData passiveGrowth;

    private Dictionary<ResourceProperty, float> properties = new Dictionary<ResourceProperty, float>();
    private Dictionary<ResourceProperty, UnityEvent<float>> propertyEvents = new Dictionary<ResourceProperty, UnityEvent<float>>();

    private void Awake()
    {
        ResourceProperty[] props = (ResourceProperty[])Enum.GetValues(typeof(ResourceProperty));
        foreach (ResourceProperty prop in props)
        {
            properties.Add(prop, 0);
            propertyEvents.Add(prop, new UnityEvent<float>());
        }
    }

    private void Start()
    {
        GameManager.DaySystem.OnDayChanged.AddListener(DayTick);
    }

    public void ChangeProperty(ResourceProperty prop, float value)
    {
        properties[prop] += value;
        propertyEvents[prop].Invoke(properties[prop]);
    }

    public float GetProperty(ResourceProperty prop)
    {
        return properties[prop];
    }

    public void AddPropertyListener(ResourceProperty prop, UnityAction<float> listener)
    {
        propertyEvents[prop].AddListener(listener);
    }

    public void DayTick(int day)
    {
        ChangeProperty(ResourceProperty.Money, GetProperty(ResourceProperty.MoneyRate));
        ChangeProperty(ResourceProperty.Population, GetProperty(ResourceProperty.PopulationRate));
        ChangeProperty(ResourceProperty.Parts, GetProperty(ResourceProperty.PartsRate));
        ChangeProperty(ResourceProperty.Food, GetProperty(ResourceProperty.FoodRate));
    }

    // private void RefreshRates()
    // {
    //     float newMoneyRate = 0;
    //     float newPopulationRate = 0;
    //     float newFuelRate = 0;
    //     float newFoodRate = 0;

    //     if (Population >= 2)
    //     {
    //         newPopulationRate += passiveGrowth.peopleRate * Population;
    //     }
    //     newFoodRate += passiveGrowth.foodRate * Population;
    //     newFuelRate += passiveGrowth.fuelRate * Population;

    //     moneyRate = newMoneyRate;
    //     populationRate = newPopulationRate;
    //     fuelRate = newFuelRate;
    //     foodRate = newFoodRate;

    //     OnMoneyRateChanged.Invoke(moneyRate);
    //     OnPopulationRateChanged.Invoke(populationRate);
    //     OnFuelRateChanged.Invoke(fuelRate);
    //     OnFoodRateChanged.Invoke(foodRate);
    // }
}
