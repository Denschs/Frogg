using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{

    [SerializeField] private PlayerInput _inputActions;
    private InputAction pauseGame;
    public Canvas pauseCanvas;
    private bool gameIsPaused = false;

    void Awake()
    {
        //pauseGame = _inputActions.actions["Menu"];
        //pauseGame.performed += _ => ResumePause();

    }

    public void ResumePause()
    {
        if (!gameIsPaused && pauseCanvas != null)
        {
            Time.timeScale = 0f;
            gameIsPaused = true;
            pauseCanvas.gameObject.SetActive(true);
            Debug.Log("Spiel Pause");
        }

        else if (gameIsPaused && pauseCanvas != null)
        {
            Time.timeScale = 1f;
            gameIsPaused = false;
            pauseCanvas.gameObject.SetActive(false);
            Debug.Log("Spiel Resume");
        }
    }
    public void MainMenu()
    {
        ResumePause();
        SceneManager.LoadScene("MainMenu");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Spiel Reset");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    /*
    private void OnEnable()
    {
        pauseGame.Enable();
    }

    private void OnDisable()
    {
        pauseGame.Disable();
    }
    */
}
