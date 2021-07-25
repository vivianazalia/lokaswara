﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public List<Tutorial> tutorials = new List<Tutorial>();

    public Text explanationText;

    private Tutorial currentTutorial;

    private int lastOrder;

    public static TutorialManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = FindObjectOfType<TutorialManager>();
        }
    }

    private void Start()
    {
        explanationText = GameObject.Find("Canvas/Explanation Text").GetComponent<Text>();
        if (!PlayerPrefs.HasKey("AppFirstRun"))
        {
            SetNextTutorial(1);
        }
        else
        {
            this.gameObject.SetActive(false);
            explanationText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (currentTutorial)
        {
            currentTutorial.CheckIfHappening();
        }
        else
        {
            explanationText.gameObject.SetActive(false);
        }
    }

    public void CompletedTutorial()
    {
        SetNextTutorial(currentTutorial.order + 1);
    }

    public void SetNextTutorial(int currentOrder)
    {
        currentTutorial = GetTutorialByOrder(currentOrder);

        if (!currentTutorial)
        {
            return;
        }

        explanationText.text = currentTutorial.explanation;
    }

    public int CheckListCount()
    {
        return tutorials.Count;
    }

    public Tutorial GetTutorialByOrder(int order)
    {
        for(int i = 0; i < tutorials.Count; i++)
        {
            if(tutorials[i].order == order)
            {
                return tutorials[i];
            }
        }

        return null;
    }
}
