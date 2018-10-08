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

    //Activate the bomb sprite
    public void SetBomb(bool dead)
    {
        if (dead)
        {
            bomb.enabled = true;
        }
        else
        {
            bomb.enabled = false;
        }
    }

    //Get the status of bomb.enabled
    public bool GetBomb()
    {
        return bomb.enabled;
    }

    private void OnMouseDown()
    {
        MeshRenderer m = GetComponent<MeshRenderer>();
        m.enabled = false;
        SetText(8);
    }
}
