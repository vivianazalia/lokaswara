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
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                Animator anim = collision.gameObject.GetComponent<Animator>();
                AudioSource sfx = GetComponent<AudioSource>();

                anim.SetBool("crash", true);
                sfx.volume = PlayerPrefs.GetFloat("SfxVolume");
                sfx.Play();

                GameManager.Instance.isGameOver = true;
            }
        }
    }
}
