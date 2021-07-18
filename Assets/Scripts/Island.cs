using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void OnMouseDown()
    {
        if (!panel.activeInHierarchy)
        {
            panel.SetActive(true);
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
