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

    public AudioSource src;
    public AudioClip srcOne;


    public void playGame()
    {
        src.PlayOneShot(srcOne);
        StartCoroutine(_PlayGame());

        //introCanvas.SetActive(true);
        //SceneManager.LoadScene("Main");
    }
    private IEnumerator _PlayGame()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Main");
        //mainMenu.SetActive(false);
    }
    public void playGameReverseScene()
    {
        src.PlayOneShot(srcOne);
        StartCoroutine(_playGameReverseScene());

        //introCanvas.SetActive(true);
        //SceneManager.LoadScene("Main");
    }
    private IEnumerator _playGameReverseScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Main_ReverseTarget");
        //mainMenu.SetActive(false);
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
