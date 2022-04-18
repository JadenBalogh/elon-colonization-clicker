using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSystem : MonoBehaviour
{
    [SerializeField] private float goalPop = 1000000;
    [SerializeField] private CanvasGroup winPanel;

    private void Start()
    {
        GameManager.ResourceSystem.AddPropertyListener(ResourceProperty.Population, CheckForEnd);
    }

    private void CheckForEnd(float pop)
    {
        if (pop >= goalPop)
        {
            winPanel.alpha = 1;
            winPanel.blocksRaycasts = true;
            winPanel.interactable = true;
            Time.timeScale = 0f;
        }
    }
}
