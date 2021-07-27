using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonTileManager : MonoBehaviour
{
    [SerializeField] private List<Transform> nonTiles = new List<Transform>();
    TilesManager tileManager;

    private void Start()
    {
        tileManager = FindObjectOfType<TilesManager>();
        Transform[] childTransform = GetComponentsInChildren<Transform>();
        foreach(Transform t in childTransform)
        {
            if(t.transform != this.transform)
            {
                nonTiles.Add(t);
            }
        }
    }

    private void Update()
    {
        SetXPosition();
    }

    Tile ActiveTile()
    { 
        if(tileManager.tiles.Count > 0)
        {
            if (tileManager.tiles[0].canPressed)
            {
                return tileManager.tiles[0];
            }       
        }
        return null;
    }


    void SetXPosition()
    {
        Tile activeTile = ActiveTile();

        foreach(Transform t in nonTiles)
        {
            if (activeTile)
            {
                t.localPosition = new Vector3(t.position.x, activeTile.transform.position.y, 0);
                t.GetComponent<BoxCollider2D>().enabled = true;

                if (t.position == activeTile.transform.position)
                {
                    t.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }
}
