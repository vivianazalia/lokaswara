using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    [SerializeField] private List<Tile> tiles = new List<Tile>();
    [SerializeField] private List<Tile> tilesPressed = new List<Tile>();
    Tile[] childTiles;

    private int multiplier = 1;
    private float speed = 5;
    private const float maxSpeed = 15;
    private const int maxStar = 3;

    void Start()
    {
        childTiles = gameObject.GetComponentsInChildren<Tile>();

        for(int i = 0; i < childTiles.Length; i++)
        {
            tiles.Add(childTiles[i]);
        }
    }

    void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            TileManage();
        }
    }

    void TileManage()
    {
        if (tiles.Count > 0)
        {
            tiles[0].canPressed = true;

            if (tiles[0].wasPressed)
            {
                tilesPressed.Add(tiles[0]);
                tiles.RemoveAt(0);
            }
        }
        else if (tiles.Count == 0)
        {
            multiplier++;
            
            if(speed >= maxSpeed)
            {
                speed = maxSpeed;
            }
            else
            {
                speed++;
            }

            if(GameManager.Instance.star >= maxStar)
            {
                GameManager.Instance.star = maxStar;
            }
            else
            {
                GameManager.Instance.star++;
            }

            foreach (Tile t in tilesPressed)
            {
                tiles.Add(t);
                t.SetSpeed(speed);
                t.ResetPosition();
                t.SetMultiplier(multiplier);
            }
            tilesPressed.Clear();
        }
    }
}
