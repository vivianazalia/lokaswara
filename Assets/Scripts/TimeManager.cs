using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    DateTime currentDate;
    DateTime lastDate;
    TimeSpan differenceTime;

    //[SerializeField] private Text datetimeNow;
    //[SerializeField] private Text datetimeLast;
    //[SerializeField] private Text differTime;

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
        if (!PlayerPrefs.HasKey("LastTime"))
        {
            lastDate = DateTime.Now;
            currentDate = DateTime.Now;
        }
        else
        {
            currentDate = DateTime.Now;
            string lastDateString = PlayerPrefs.GetString("LastTime");
            lastDate = DateTime.Parse(lastDateString);
        }
    }

    public int DifferenceSeconds()
    {
        //datetimeNow.text = "CurrentTime : " + currentDate.ToString();
        //datetimeLast.text = "LastTime : " + lastDate.ToString();
        if(currentDate > lastDate)
        {
            differenceTime = currentDate - lastDate;
            //differTime.text = "Difference Time in Sec : " + differenceTime.TotalSeconds.ToString();
        }
        return (int)differenceTime.TotalSeconds;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LastTime", DateTime.Now.ToString());
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (!PlayerPrefs.HasKey("LastTime"))
            {
                lastDate = DateTime.Now;
                currentDate = DateTime.Now;
            }
            else
            {
                currentDate = DateTime.Now;
                string lastDateString = PlayerPrefs.GetString("LastTime");
                lastDate = DateTime.Parse(lastDateString);
            }
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            if (!PlayerPrefs.HasKey("LastTime"))
            {
                lastDate = DateTime.Now;
                currentDate = DateTime.Now;
            }
            else
            {
                currentDate = DateTime.Now;
                string lastDateString = PlayerPrefs.GetString("LastTime");
                lastDate = DateTime.Parse(lastDateString);
            }
        }
    }
}
