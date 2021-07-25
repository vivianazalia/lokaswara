using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MediumTile : Tile
{
    private float currentTimer = 0;

    protected override void Update()
    {
        base.Update();

        if (currentTimer < timer && isPressed)
        {
            currentTimer += timer * Time.deltaTime;
        }
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
    }

    protected override void OnMouseUp()
    {
        if (!GameManager.Instance.isGameOver && !EventSystem.current.IsPointerOverGameObject())
        {
            isPressed = false;
            if (canPressed)
            {
                if (currentTimer >= 2)
                {
                    GameManager.Instance.score += multiplier * 2;
                }
                else
                {
                    GameManager.Instance.score += multiplier * 1;
                }

                currentTimer = 0;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                wasPressed = true;
            }
        }
    }
}
