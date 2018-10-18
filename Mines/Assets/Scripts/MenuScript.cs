using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas instructions;

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
	
    public void Instructions()
    {
        mainMenu.gameObject.SetActive(false);
        instructions.gameObject.SetActive(true);
    }

    public void ReturnToMenu()
    {
        instructions.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
}
