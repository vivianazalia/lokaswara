using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    DateTime currentDate;
    DateTime lastDate;
    TimeSpan differenceTime;

    public static TimeManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        string lastDateString = PlayerPrefs.GetString("LastTime");
        if (!lastDateString.Equals(""))
        {
            lastDate = DateTime.Parse(lastDateString);
            currentDate = DateTime.Now;
            
        }
        else
        {
            currentDate = DateTime.Now;
            lastDate = DateTime.Now;
        }
    }

    public int DifferenceSeconds()
    {
        if(currentDate > lastDate)
        {
            differenceTime = currentDate - lastDate;
        }
        return (int)differenceTime.TotalSeconds;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LastTime", DateTime.Now.ToString());
    }
}
