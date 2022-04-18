using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceSystem : MonoBehaviour
{
    [SerializeField] private UpgradeData passiveGrowth;
    [SerializeField] private PropertyCap[] propertyCaps;
    [SerializeField] private BuildingDisplay[] buildings;
    [SerializeField] private float foodRatePerPerson;
    [SerializeField] private float starvationDeathRate;
    [SerializeField] private float reproductionRate;

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
        AddPropertyListener(ResourceProperty.Population, (pop) => RefreshProps());
        AddPropertyListener(ResourceProperty.Food, (food) => RefreshProps());
    }

    public void SetProperty(ResourceProperty prop, float value)
    {
        properties[prop] = value;
        propertyEvents[prop].Invoke(properties[prop]);
    }

    public void ChangeProperty(ResourceProperty prop, float value)
    {
        properties[prop] += value;

        float propCap = GetCap(prop);
        if (propCap >= 0)
        {
            properties[prop] = Mathf.Clamp(properties[prop], 0f, propCap);
        }

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
        ChangeProperty(ResourceProperty.Minerals, GetProperty(ResourceProperty.MineralsRate));
    }

    public void RefreshProps()
    {
        SetProperty(ResourceProperty.MoneyRate, 0);
        SetProperty(ResourceProperty.PopulationRate, 0);
        SetProperty(ResourceProperty.PartsRate, 0);
        SetProperty(ResourceProperty.FoodRate, 0);
        SetProperty(ResourceProperty.MineralsRate, 0);

        SetProperty(ResourceProperty.MoneyCap, 0);
        SetProperty(ResourceProperty.PopulationCap, 0);
        SetProperty(ResourceProperty.PartsCap, 0);
        SetProperty(ResourceProperty.FoodCap, 0);
        SetProperty(ResourceProperty.MineralsCap, 0);

        int pop = Mathf.FloorToInt(GetProperty(ResourceProperty.Population));
        float popRoot = Mathf.Pow(pop, 2f / 3f);

        ChangeProperty(ResourceProperty.FoodRate, popRoot * foodRatePerPerson);

        if (GetProperty(ResourceProperty.Food) <= 0)
        {
            ChangeProperty(ResourceProperty.PopulationRate, popRoot * starvationDeathRate);
        }

        if (pop >= 2)
        {
            ChangeProperty(ResourceProperty.PopulationRate, pop * reproductionRate);
        }

        foreach (BuildingDisplay building in buildings)
        {
            building.ApplyResources();
        }
    }

    private float GetCap(ResourceProperty prop)
    {
        foreach (PropertyCap cap in propertyCaps)
        {
            if (cap.property == prop)
            {
                return GetProperty(cap.propertyCap);
            }
        }
        return -1;
    }

    [Serializable]
    private class PropertyCap
    {
        public ResourceProperty property;
        public ResourceProperty propertyCap;
    }
}
