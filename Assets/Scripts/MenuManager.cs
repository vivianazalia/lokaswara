using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject exitPanel;

    [Header("Setting")]
    [SerializeField] private GameObject settingPanel;

    [Header("Credit")]
    [SerializeField] private GameObject creditPanel;

    [Header("Tutorial")]
    [SerializeField] private GameObject tutorialPanel;

    [SerializeField] private GameObject cutscene;
    [SerializeField] private VideoPlayer video;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioSource bgm;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("AppFirstRun"))
        {
            PlayerPrefs.SetFloat("BgmVolume", 1);
            PlayerPrefs.SetFloat("SfxVolume", 1);
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
            PlayerPrefs.SetInt("Heart", 5);
            PlayerPrefs.SetInt("TotalExp", 350);
            PlayerPrefs.SetInt("TimerCountDown", 120);

            //play cutscene
            bgm.Stop();
            video.gameObject.SetActive(true);
            cutscene.SetActive(true);
            Debug.Log("Play Cutscene");
            video.loopPointReached += CheckVideoOver;
        }
        else
        {
            SceneManager.LoadScene("Map");
        }
    }

    public void SkipVideo()
    {
        SceneManager.LoadScene("Map");
    }

    void CheckVideoOver(VideoPlayer vp)
    {
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

    public void Tutorial()
    {
        sfx.Play();
        tutorialPanel.SetActive(true);
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
        else if (tutorialPanel.activeInHierarchy)
        {
            tutorialPanel.SetActive(false);
        }
    }

    void Quit()
    {
        Application.Quit();
    }
}
