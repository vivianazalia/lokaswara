using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    [SerializeField] private float speed;
    protected float timer;
    Vector3 startPosition;

    public bool wasPressed = false;
    public bool canPressed = false;
    [SerializeField] protected bool isPressed = false;

    [SerializeField] protected Sprite normalSprite;
    [SerializeField] protected Sprite pressedSprite;
    [SerializeField] protected AudioSource audio;

    public const int score = 1;
    protected int multiplier = 1;

    void Start()
    {
        startPosition = transform.position;
        timer = normalSprite.rect.height / (speed * 30);
        gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
    }

    protected virtual void Update()
    {
        if (!GameManager.Instance.isGameOver && GameManager.Instance.startScroll)
        { 
            ScrollTile();
        }
    }

    void ScrollTile()
    {
        transform.position -= (new Vector3(0, speed * Time.deltaTime, 0));
    }

    protected virtual void OnMouseDown()
    {
        if (!GameManager.Instance.isGameOver && !EventSystem.current.IsPointerOverGameObject())
        {
            isPressed = true;
            if (canPressed)
            {
                audio.Play();
                gameObject.GetComponent<SpriteRenderer>().sprite = pressedSprite;
            }
        }
    }

    protected virtual void OnMouseUp()
    {
        if (!GameManager.Instance.isGameOver && !EventSystem.current.IsPointerOverGameObject())
        {
            isPressed = false;
            if (canPressed)
            {
                GameManager.Instance.score += multiplier * score;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                wasPressed = true;
            }
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        gameObject.SetActive(true);
        wasPressed = false;
        canPressed = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public bool IsPressed()
    {
        return isPressed;
    }

    public void SetMultiplier(int m)
    {
        multiplier = m;
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }
}
