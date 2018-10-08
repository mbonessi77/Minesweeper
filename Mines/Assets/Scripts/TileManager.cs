using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //Variable declaration
    [SerializeField] private GameObject tile;
    private GameObject[,] tiles = new GameObject[8, 14];
    private int totalBombs;

    // Use this for initialization
    void Start ()
    {
        totalBombs = 0;
        SetTiles();
        while(totalBombs < 20)
        {
            SetBombs();
        }
	}

    //Populate the board with clickable tiles
    void SetTiles()
    {
        for(int i = 0; i < tiles.GetLength(1); i++)
        {
            for(int k = 0; k < tiles.GetLength(0); k++)
            {
                tiles[k, i] = Instantiate(tile, new Vector2(i, k), Quaternion.identity);
            }
        }
    }

    //Place 20-30 bombs randomly on the tiles
    void SetBombs()
    {
        for(int i = 0; i < tiles.GetLength(1); i++)
        {
            for(int k = 0; k < tiles.GetLength(0); k++)
            {
                if(Random.Range(0, 10) == 7 && !tiles[k, i].GetComponent<TileScript>().GetBomb())
                {
                    tiles[k, i].GetComponent<TileScript>().SetBomb(true);
                    totalBombs++;
                }
            }
        }
    }
}
