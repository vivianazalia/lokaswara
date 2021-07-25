using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSongTutorial : Tutorial
{
    [SerializeField] private SongUI panelJawa;

    public override void CheckIfHappening()
    {
        DontDestroyOnLoad(this);
        if (!PlayerPrefs.HasKey("AppFirstRun"))
        {
            if (panelJawa.isClicked)
            {
                TutorialManager.Instance.CompletedTutorial();
            }
        }
    }
}
