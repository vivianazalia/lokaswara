using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int order;

    [TextArea]
    public string explanation;

    private void Awake()
    {
        TutorialManager.Instance.tutorials.Add(this);
    }

    private void Start()
    { 
        
    }
    public virtual void CheckIfHappening()
    {

    }
}
