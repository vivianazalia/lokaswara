using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClickTutorial : Tutorial
{
    private bool isClicked;

    private void Start()
    {
        if (PlayerPrefs.HasKey("AppFirstRun"))
        {
            GetComponent<Animator>().enabled = false;
        }
    }

    public override void CheckIfHappening()
    {
        if (!PlayerPrefs.HasKey("AppFirstRun"))
        {
            if (isClicked)
            {
                TutorialManager.Instance.CompletedTutorial();
            }
        }
    }

    private void OnMouseDown()
    {
        isClicked = true;
    }
}
