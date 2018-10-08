using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //Variable declaration
    [SerializeField] private GameObject tile;

	// Use this for initialization
	void Start ()
    {
		for(int i = 0; i < 14; i++)
        {
            for(int k = 0; k < 8; k++)
            {
                Instantiate(tile, new Vector2(i, k), Quaternion.identity);
            }
        }
	}
}
