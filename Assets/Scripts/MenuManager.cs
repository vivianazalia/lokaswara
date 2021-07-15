using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void OnMouseDown()
    {
        if (!PlayerPrefs.HasKey("AppFirstRun"))
        {
            //configure 
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("Exp Point", 0);
            PlayerPrefs.SetInt("Heart", 3);

            //play cutscene
            Debug.Log("Play Cutscene");
        }
        SceneManager.LoadScene("Map");
    }
}
