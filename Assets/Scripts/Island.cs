using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Island : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (!panel.activeInHierarchy)
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
}
