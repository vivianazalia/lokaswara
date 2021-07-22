using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnBeat : MonoBehaviour
{
    public SoundManager soundManager;
    public AudioClip tap;
    //public AudioClip[] strum;
    //private int randomStrum;

    private void Update()
    {
        if (BPeerM.isBeatD4)
        {
            soundManager.PlaySound(tap, 0.5f);
        
            //if(BPeerM.beatCountFull % 2 == 0)
            //{
            //    randomStrum = Random.Range(0, strum.Length);
            //}
        }

        // beatCountD8 % 2 mean play each 1/4 not -> 1/8 * 2 = 1/4
        //if(BPeerM.isBeatD8 && BPeerM.beatCountD8 % 2 == 0)
        //{
        //    soundManager.PlaySound(tick, 0.3f);
        //}
        //
        //// play when each 1/4 or 1/2 not
        //if(BPeerM.isBeatD8 && (BPeerM.beatCountD8 % 8 == 2 || BPeerM.beatCountD8 % 8 == 4))
        //{
        //    soundManager.PlaySound(strum[randomStrum], 1);
        //}
    }
}
