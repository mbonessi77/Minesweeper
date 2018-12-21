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
        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = i + x;
                int checkY = k + y;

                if (checkX >= 0 && checkX < tiles.GetLength(1) && checkY >= 0 && checkY < tiles.GetLength(0))
                {
                    if(tiles[checkY, checkX].GetComponent<TileScript>().GetBomb())
                    {
                        Debug.Log("Bomb found");
                        num++;
                    }
                }
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