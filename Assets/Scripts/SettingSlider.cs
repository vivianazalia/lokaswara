using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlider : MonoBehaviour
{
    private float valueBgm;
    private float valueSfx;
    [SerializeField] private Slider sliderBGM;
    [SerializeField] private Slider sliderSFX;

    private void Start()
    {
        valueBgm = PlayerPrefs.GetFloat("BgmVolume");
        valueSfx = PlayerPrefs.GetFloat("SfxVolume");
        sliderBGM.value = valueBgm;
        sliderSFX.value = valueSfx;
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("BgmVolume", sliderBGM.value);
        PlayerPrefs.SetFloat("SfxVolume", sliderSFX.value);
    }
}
