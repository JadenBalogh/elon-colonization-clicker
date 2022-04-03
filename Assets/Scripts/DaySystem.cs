using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DaySystem : MonoBehaviour
{
    [SerializeField] private float dayDuration = 1f;

    public int Day { get; private set; }
    public UnityEvent<int> OnDayChanged { get; private set; }

    private void Awake()
    {
        OnDayChanged = new UnityEvent<int>();
    }

    private void Start()
    {
        StartCoroutine(DayLoop());
    }

    private IEnumerator DayLoop()
    {
        WaitForSeconds dayWait = new WaitForSeconds(dayDuration);
        while (true)
        {
            Day++;
            OnDayChanged.Invoke(Day);

            yield return dayWait;
        }
    }
}
