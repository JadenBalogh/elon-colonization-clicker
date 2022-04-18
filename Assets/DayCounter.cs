using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayCounter : MonoBehaviour
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
        textbox.text = "You completed this attempt in " + day + " days.";
    }
}
