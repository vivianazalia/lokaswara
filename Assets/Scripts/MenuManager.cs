using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject exitPanel;

    [Header("Setting")]
    [SerializeField] private GameObject settingPanel;

    [Header("Credit")]
    [SerializeField] private GameObject creditPanel;

    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioSource bgm;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("AppFirstRun"))
        {
            PlayerPrefs.SetFloat("BgmVolume", 0.5f);
            PlayerPrefs.SetFloat("SfxVolume", 0.5f);
        }
        bgm.volume = PlayerPrefs.GetFloat("BgmVolume");
        sfx.volume = PlayerPrefs.GetFloat("SfxVolume");
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShowExitPanel();
            }
        }

        bgm.volume = PlayerPrefs.GetFloat("BgmVolume");
        sfx.volume = PlayerPrefs.GetFloat("SfxVolume");
    }
    public void ResetFirstRun()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Reset Succes");
    }

    public void Play()
    {
        sfx.Play();
        if (!PlayerPrefs.HasKey("AppFirstRun"))
        {
            //configure 
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("Exp Point", 0);
            PlayerPrefs.SetInt("Heart", 3);
            PlayerPrefs.SetInt("TotalExp", 100);
            PlayerPrefs.SetInt("TimerCountDown", 180);

            //play cutscene
            Debug.Log("Play Cutscene");
        }
        SceneManager.LoadScene("Map");
    }

    public void ShowExitPanel()
    {
        if (!exitPanel.activeInHierarchy)
        {
            exitPanel.SetActive(true);
        }
    }

    public void YaButton()
    {
        sfx.Play();
        Quit();
    }

    public void TidakButton()
    {
        sfx.Play();
        if (exitPanel.activeInHierarchy)
        {
            exitPanel.SetActive(false);
        }
    }

    public void Setting()
    {
        sfx.Play();
        settingPanel.SetActive(true);
    }

    public void Credit()
    {
        sfx.Play();
        creditPanel.SetActive(true);
    }

    public void Close()
    {
        sfx.Play();
        if (settingPanel.activeInHierarchy)
        {
            settingPanel.SetActive(false);
        }
        else if (creditPanel.activeInHierarchy)
        {
            creditPanel.SetActive(false);
        }
    }

    void Quit()
    {
        Application.Quit();
    }
}
