using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPeerM : MonoBehaviour
{
    public static BPeerM Instance;

    public float bpm;
    public float multiplier;
    public float beatInterval, beatTimer;
    private float beatIntervalD2, beatTimerD2;
    private float beatIntervalD4, beatTimerD4;
    private float beatIntervalD8, beatTimerD8;

    public static bool isBeatFull, isBeatD2, isBeatD4, isBeatD8;
    public static int beatCountFull, beatCountD2, beatCountD4, beatCountD8;

    private void Awake()
    {
        beatInterval = 60 / bpm;
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        WholeDuration();
        HalfDuration();
        QuarterDuration();
        EighthDuration();
    }

    void WholeDuration()
    {
        isBeatFull = false;
        beatInterval = 60 / bpm;
        beatTimer += Time.deltaTime;

        if(beatTimer >= beatInterval)
        {
            beatTimer -= beatInterval;
            isBeatFull = true;
            beatCountFull++;
        }
    }

    void HalfDuration()
    {
        // 1 / 2 not
        isBeatD2 = false;
        beatIntervalD2 = beatInterval / 2;
        beatTimerD2 += Time.deltaTime;
    
        if (beatTimerD2 >= beatIntervalD2)
        {
            beatTimerD2 -= beatIntervalD2;
            isBeatD2 = true;
            beatCountD2++;
        }
    }
    
    void QuarterDuration()
    {
        // 1 / 4 not
        isBeatD4 = false;
        beatIntervalD4 = beatInterval / 4;
        beatTimerD4 += Time.deltaTime;
    
        if (beatTimerD4 >= beatIntervalD4)
        {
            beatTimerD4 -= beatIntervalD4;
            isBeatD4 = true;
            beatCountD4++;
        }
    }
    
    void EighthDuration()
    {
        // 1 / 8 not
        isBeatD8 = false;
        beatIntervalD8 = beatInterval / 8;
        beatTimerD8 += Time.deltaTime;
    
        if (beatTimerD8 >= beatIntervalD8)
        {
            beatTimerD8 -= beatIntervalD8;
            isBeatD8 = true;
            beatCountD8++;
        }
    }
}

