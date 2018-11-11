using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileScript : MonoBehaviour
{
    //Variable declaration
    public SpriteRenderer bomb;
    public TextMesh number;
    private int numBombs;
    private bool hasBomb;
    private int numTiles;

	// Use this for initialization
	void Start ()
    {
        numTiles = 14 * 8;
        number.text = "";
        bomb.enabled = false;
	}

    //Setters
    public void SetText(int num)
    {
        number.text = num.ToString();
    }

    public void SetBomb(bool dead)
    {
        hasBomb = dead;
    }

    public void SetNumBombs(int num)
    {
        numBombs = num;
    }

    //Getters
    public int GetNumBombs()
    {
        return numBombs;
    }

    public bool GetBomb()
    {
        return hasBomb;
    }

    public int GetTilesLeft()
    {
        return numTiles;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && number.text != "X")
        {
            //Turn off the renderer for the tile
            MeshRenderer m = GetComponent<MeshRenderer>();
            m.enabled = false;

            //Check if this tile has a bomb or not
            if (!hasBomb)
            {
                SetText(numBombs);
                numTiles--;
            }
            else
            {
                bomb.enabled = true;
            }
        }
        //Mark the tile with a right click
        if (Input.GetMouseButtonDown(1))
        {
            if (number.text != "X")
            {
                number.text = "X";
            }
            else
            {
                number.text = "";
            }
        }
        
    }
}
