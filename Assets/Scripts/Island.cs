using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Island : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private int levelToUnlock;
    private bool isUnlock = true;

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            
            if (!panel.activeInHierarchy && isUnlock)
            {
                panel.SetActive(true);
            }
        }
    }

    public void Close()
    {
        if (panel.activeInHierarchy)
        {
            panel.SetActive(false);
        }
    }

    public int GetLevelToUnlock()
    {
        return levelToUnlock;
    }

    public void IsUnlock(bool state)
    {
        isUnlock = state;
    }
}
