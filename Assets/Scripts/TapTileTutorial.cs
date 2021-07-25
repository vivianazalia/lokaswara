using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTileTutorial : Tutorial
{
    private bool isClicked;
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
