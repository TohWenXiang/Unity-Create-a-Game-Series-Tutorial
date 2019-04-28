using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    float timeleft;
    float currentTime;
    public event System.Action OnTimerOver;

    public Timer(float seconds)
    {
        ResetTimer(seconds);
    }

    public void UpdateTimer()
    {
        timeleft -= Time.deltaTime;
        if (timeleft <= 0)
        {
            if (OnTimerOver != null)
            {
                OnTimerOver();
            }
        }
    }

    void ResetTimer(float seconds)
    {
        timeleft = seconds;
    }
}
