using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject introCanvas;
    public GameObject creditsConvas;
    public GameObject howToConvas;
    public void playGame()
    {
        //introCanvas.SetActive(true);
        mainMenu.SetActive(false);

        SceneManager.LoadScene("_Main");
    }
    
    public void showCredits()
    {
        mainMenu.SetActive(false);
        creditsConvas.SetActive(true);
    }
    public void showHowTo()
    {
        mainMenu.SetActive(false);
        howToConvas.SetActive(true);
    }
    public void hideHowTos()
    {
        mainMenu.SetActive(true);
        howToConvas.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("Spiel wurde beendet!!");
        Application.Quit();
    }

    public void hideCredits()
    {
        mainMenu.SetActive(true);
        creditsConvas.SetActive(false);
    }
}
