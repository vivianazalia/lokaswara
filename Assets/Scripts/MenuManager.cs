using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject exitPanel;

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShowExitPanel();
            }
        }
    }
    public void ResetFirstRun()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Reset Succes");
    }

    public void Play()
    {
        if (!PlayerPrefs.HasKey("AppFirstRun"))
        {
            //configure 
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("Exp Point", 0);
            PlayerPrefs.SetInt("Heart", 3);
            PlayerPrefs.SetInt("TotalExp", 100);
            PlayerPrefs.SetInt("TimerCountDown", 180);
            PlayerPrefs.SetFloat("BgmVolume", 0.5f);
            PlayerPrefs.SetFloat("SfxVolume", 0.5f);

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
        Quit();
    }

    public void TidakButton()
    {
        if (exitPanel.activeInHierarchy)
        {
            exitPanel.SetActive(false);
        }
    }

    void Quit()
    {
        Application.Quit();
    }
}
