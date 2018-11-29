using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    //Variable declaration
    [SerializeField] private GameObject tile;
    [SerializeField] private Canvas endCanvas;
    [SerializeField] private Text timeText;
    private GameObject[,] tiles = new GameObject[8, 14];
    private int totalBombs;
    private static bool gameOver;
    private static float timer;

    // Use this for initialization
    void Start()
    {
        Debug.Log(gameOver);
        if (!gameOver)
        {
            SetTiles();
            SetBombs();
            SetSurroundingBombs();
            endCanvas.gameObject.SetActive(false);
        }
        else
        {
            timer += 0f;
            endCanvas.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        timeText.text = "Final Time: " + timer.ToString("F2");

        if (Input.GetKeyDown(KeyCode.R))
        {
            //SceneManager.LoadScene("SampleScene");
            gameOver = false;
            timer = 0;
            SceneManager.LoadScene("SampleScene");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main Menu");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!gameOver)
        {
            timer += Time.deltaTime;
            CheckGameOver();
        }
        else
        {
            timer += 0;
            gameOver = true;
            StartCoroutine(LoadEndScreen());
        }

        if (tiles[0, 0].GetComponent<TileScript>().GetTilesLeft() == 0)
        {
            timer += 0;
            gameOver = true;
            StartCoroutine(LoadEndScreen());
        }
    }

    //Check to see if the player has hit a bomb
    void CheckGameOver()
    {
        for(int i = 0; i < tiles.GetLength(1); i++)
        {
            for(int k = 0; k < tiles.GetLength(0); k++)
            {
                if(tiles[k, i].GetComponent<TileScript>().bomb.enabled)
                {
                    gameOver = true;
                    EndGame();
                    break;
                }
            }
        }
    }
    
    //Show the whole grid's data once the game has ended
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
                    tiles[k, i].GetComponent<TileScript>().number.text = GetSurroundingBombs(i, k).ToString();
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

    IEnumerator LoadEndScreen()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("End Screen");
    }
}