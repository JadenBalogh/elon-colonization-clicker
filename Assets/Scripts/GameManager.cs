using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static ResourceSystem ResourceSystem { get; private set; }
    public static ShipSystem ShipSystem { get; private set; }
    public static DaySystem DaySystem { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        ResourceSystem = GetComponent<ResourceSystem>();
        ShipSystem = GetComponent<ShipSystem>();
        DaySystem = GetComponent<DaySystem>();
    }
}
