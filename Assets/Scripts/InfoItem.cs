using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InfoItem : MonoBehaviour
{
    public GameObject info;

    public virtual void OnMouseDown()
    {
        if (!info.activeInHierarchy)
        {
            info.SetActive(true);
        }
        else
        {
            info.SetActive(false);
        }
    }
}
