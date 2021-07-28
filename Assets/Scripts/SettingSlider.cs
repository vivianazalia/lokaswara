using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlider : MonoBehaviour
{
    private float valueBgm;
    private float valueSfx;
    public Slider sliderBGM;
    public Slider sliderSFX;

    [SerializeField] private AudioSource audio;

    private void Start()
    {
        valueBgm = PlayerPrefs.GetFloat("BgmVolume");
        valueSfx = PlayerPrefs.GetFloat("SfxVolume");
        sliderBGM.value = valueBgm;
        sliderSFX.value = valueSfx;
        audio.volume = PlayerPrefs.GetFloat("SfxVolume");
    }

    public void SaveData()
    {
        audio.Play();
        PlayerPrefs.SetFloat("BgmVolume", sliderBGM.value);
        PlayerPrefs.SetFloat("SfxVolume", sliderSFX.value);
    }
}
