using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NonTile : MonoBehaviour
{
    [SerializeField] private Sprite wrongSprite;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.sprite = wrongSprite;
            anim.Play("NonTile");
            GameManager.Instance.startScroll = false;
            GameManager.Instance.isGameOver = true;
        }
    }
}
