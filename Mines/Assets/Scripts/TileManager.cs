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
    void Start()
    {
        totalBombs = 0;
        SetTiles();
        SetBombs();
        SetSurroundingBombs();
    }

    //Show the whole grid's data
    void EndGame()
    {
        for (int i = 0; i < tiles.GetLength(1); i++)
        {
            for (int k = 0; k < tiles.GetLength(0); k++)
            {
                tiles[k, i].GetComponent<MeshRenderer>().enabled = false;
                if (tiles[k, i].GetComponent<TileScript>().GetBomb())
                {
                    tiles[k, i].GetComponent<TileScript>().bomb.enabled = true;
                }
                else
                {
                    tiles[k, i].GetComponent<TileScript>().SetNumBombs(GetSurroundingBombs(i, k));
                }
            }
        }
    }

    //Populate the board with clickable tiles
    void SetTiles()
    {
        for (int i = 0; i < tiles.GetLength(1); i++)
        {
            for (int k = 0; k < tiles.GetLength(0); k++)
            {
                tiles[k, i] = Instantiate(tile, new Vector2(i, k), Quaternion.identity);
            }
        }
    }

    //Place 20 bombs randomly on the tiles
    void SetBombs()
    {
        for (int i = 0; i < tiles.GetLength(1); i++)
        {
            for (int k = 0; k < tiles.GetLength(0); k++)
            {
                if (totalBombs == 20)
                {
                    break;
                }
                if (Random.Range(0, 5) == 1 && !tiles[k, i].GetComponent<TileScript>().GetBomb())
                {
                    tiles[k, i].GetComponent<TileScript>().SetBomb(true);
                    totalBombs++;
                }
            }
        }
        if (totalBombs < 20)
        {
            SetBombs();
        }
    }

    //Set the number of bombs surrounding a tile
    void SetSurroundingBombs()
    {
        for (int i = 0; i < tiles.GetLength(1); i++)
        {
            for (int k = 0; k < tiles.GetLength(0); k++)
            {
                if (!tiles[k, i].GetComponent<TileScript>().GetBomb())
                {
                    tiles[k, i].GetComponent<TileScript>().SetNumBombs(GetSurroundingBombs(i, k));
                }
            }
        }
    }

    //Count the number of bombs surrounding a tile
    int GetSurroundingBombs(int i, int k)
    {
        int num = 0;
        //If the current tile is on the left most column
        if (k == 0)
        {
            if (i == 0)
            {
                if (tiles[k + 1, i].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k + 1, i + 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k, i + 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
            }
            else if (i == tiles.GetLength(1) - 1)
            {
                if (tiles[k + 1, i].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k + 1, i - 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k, i - 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
            }
            else
            {
                if (tiles[k, i + 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k + 1, i + 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k + 1, i].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k + 1, i - 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k, i - 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
            }
        }

        //If the current tile is on the right most column
        else if (k == tiles.GetLength(0) - 1)
        {
            if (i == 0)
            {
                if (tiles[k - 1, i].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k - 1, i + 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k, i + 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
            }
            else if (i == tiles.GetLength(1) - 1)
            {
                if (tiles[k - 1, i].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k - 1, i - 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k, i - 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
            }
            else
            {
                if (tiles[k, i + 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k - 1, i + 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k - 1, i].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k - 1, i - 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
                if (tiles[k, i - 1].GetComponent<TileScript>().GetBomb())
                {
                    num++;
                }
            }
        }

        //If the current tile is on the bottom most row
        else if (i == 0 && !(k == 0 || k == tiles.GetLength(0) - 1))
        {
            if (tiles[k - 1, i].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k - 1, i + 1].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k, i + 1].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k + 1, i + 1].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k + 1, i].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
        }

        //If the current tile is on the top most row
        else if (i == tiles.GetLength(1) - 1 && !(k == 0 || k == tiles.GetLength(0) - 1))
        {
            if (tiles[k - 1, i].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k - 1, i - 1].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k, i - 1].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k + 1, i - 1].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k + 1, i].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
        }

        //If the current tile isn't on an edge or corner
        else
        {
            if (tiles[k - 1, i - 1].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k - 1, i].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k - 1, i + 1].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k, i + 1].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k + 1, i + 1].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k + 1, i].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k + 1, i - 1].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
            if (tiles[k, i - 1].GetComponent<TileScript>().GetBomb())
            {
                num++;
            }
        }
        return num;
    }
}