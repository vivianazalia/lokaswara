using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public int bankSize;
    public List<AudioSource> soundClip;

    private void Start()
    {
        soundClip = new List<AudioSource>();
        for(int i = 0; i < bankSize; i++)
        {
            GameObject soundInstance = new GameObject("Sound");
            soundInstance.AddComponent<AudioSource>();
            soundInstance.GetComponent<AudioSource>().playOnAwake = false;
            soundInstance.transform.parent = this.transform;
            soundClip.Add(soundInstance.GetComponent<AudioSource>());
        }
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        for(int i = 0; i < soundClip.Count; i++)
        {
            if (!soundClip[i].isPlaying)
            {
                soundClip[i].clip = clip;
                soundClip[i].volume = volume;
                soundClip[i].Play();
                return;
            }
        }

        //buat sound gameobject baru saat semua soundclip sedang memainkan lagu
        GameObject soundInstance = new GameObject("Sound");
        soundInstance.AddComponent<AudioSource>();
        soundInstance.transform.parent = this.transform;
        soundInstance.GetComponent<AudioSource>().playOnAwake = false;
        soundInstance.GetComponent<AudioSource>().clip = clip;
        soundInstance.GetComponent<AudioSource>().volume = volume;
        soundInstance.GetComponent<AudioSource>().Play();
        soundClip.Add(soundInstance.GetComponent<AudioSource>());
    }
}
