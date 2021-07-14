using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            if (!collision.gameObject.GetComponent<Tile>().IsPressed())
            {
                GameManager.Instance.isGameOver = true;
            }
        }
    }
}
