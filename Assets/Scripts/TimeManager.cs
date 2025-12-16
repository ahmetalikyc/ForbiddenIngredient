using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static Action OnDayChanged;

    public static int Minute { get; private set; } 
    public static int Hour { get; private set; }
    public static int Day { get; private set; }

    private float minuteToRealTime = 0.5f;
    private float timer;

    void Start()
    {
        Minute = 55;
        Hour = 11;
        Day = 1;
        timer = minuteToRealTime;
    }


    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();
            if (Minute >= 60)
            {
                Hour++;
                OnHourChanged?.Invoke();
                Minute = 0;
                if (Hour == 12)
                {
                    Debug.Log("food service");
                }
                if (Hour == 18)
                {
                    Debug.Log("food delivery");
                }
                if (Hour >= 24)
                {
                    Day++;
                    OnDayChanged?.Invoke();
                    Hour = 0;
                }

            }
            timer =minuteToRealTime;
        }
    }
}
