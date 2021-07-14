using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private float speed;
    protected float timer;
    private Vector3 movementValue;
    public bool wasPressed = false;
    public bool canPressed = false;
    [SerializeField] protected bool isPressed = false;
    Vector3 startPosition;
    [SerializeField] protected Sprite normalSprite;
    [SerializeField] protected Sprite pressedSprite;

    public const int score = 1;

    void Start()
    {
        startPosition = transform.position;
        timer = normalSprite.rect.height / (speed * 30);
        movementValue = new Vector3(0, speed, 0);
        gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
    }

    protected virtual void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            ScrollTile();
        }
    }

    void ScrollTile()
    {
        transform.position -= movementValue * Time.deltaTime;
    }

    protected virtual void OnMouseDown()
    {
        if (!GameManager.Instance.isGameOver)
        {
            isPressed = true;
            if (canPressed)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = pressedSprite;
            }
        }
    }

    protected virtual void OnMouseUp()
    {
        if (!GameManager.Instance.isGameOver)
        {
            isPressed = false;
            if (canPressed)
            {
                GameManager.Instance.score += score;
                gameObject.SetActive(false);
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
    }

    public bool IsPressed()
    {
        return isPressed;
    }
}
