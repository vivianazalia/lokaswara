using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongTile : Tile
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
        if (!GameManager.Instance.isGameOver)
        {
            isPressed = false;
            if (canPressed)
            {
                if (currentTimer >= 2)
                {
                    GameManager.Instance.score += multiplier * 4;
                }
                else
                {
                    GameManager.Instance.score += multiplier * 1;
                }

                currentTimer = 0;
                gameObject.SetActive(false);
                wasPressed = true;
            }
        }
    }
}
