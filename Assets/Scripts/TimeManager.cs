using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    DateTime currentDate;

    private static TimeManager instance = null;

    public static TimeManager Instance { get { return instance; } }

    public float CheckDate()
    {
        currentDate = DateTime.Now;

        string tempString = PlayerPrefs.GetString("Save Date");
        long tempLong = Convert.ToInt64(tempString);
    }
}
