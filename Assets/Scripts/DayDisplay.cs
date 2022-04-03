using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayDisplay : MonoBehaviour
{
    private TextMeshProUGUI textbox;

    private void Awake()
    {
        textbox = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.DaySystem.OnDayChanged.AddListener(UpdateDisplay);
    }

    private void UpdateDisplay(int day)
    {
        int year = day / 365 + 1;
        int actualDay = day % 365;
        textbox.text = $"Year {year}, Day {actualDay}";
    }
}
