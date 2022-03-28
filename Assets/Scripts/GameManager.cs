using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private RectTransform canvas;
    public static RectTransform Canvas { get => instance.canvas; }

    public static ResourceSystem ResourceSystem { get; private set; }
    public static ShipSystem ShipSystem { get; private set; }

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
    }
}
