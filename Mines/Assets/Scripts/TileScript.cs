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

	// Use this for initialization
	void Start ()
    {
        number.text = "";
        bomb.enabled = false;
        numBombs = 0;
	}

    //Set the text to how many bombs surround the current tile
    public void SetText(int num)
    {
        number.text = num.ToString();
    }

    //Gives the bomb a tile if true
    public void SetBomb(bool dead)
    {
        hasBomb = dead;
    }

    //Get the status of bomb.enabled
    public bool GetBomb()
    {
        return hasBomb;
    }

    private void OnMouseDown()
    {
        //Turn off the renderer to the tile
        MeshRenderer m = GetComponent<MeshRenderer>();
        m.enabled = false;

        //
        if (!hasBomb)
        {
            SetText(numBombs);
        }
        else
        {
            bomb.enabled = true;
        }
    }
}
