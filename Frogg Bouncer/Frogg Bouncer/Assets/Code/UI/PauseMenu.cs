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
    bool deactivate = false;

    void Awake()
    {
        //pauseGame = _inputActions.actions["Menu"];
        //pauseGame.performed += _ => ResumePause();
        CodeEventHandler.GameEnded += GameEnded;

    }

    public void ResumePause()
    {
        if (!deactivate)
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
       
    }
    public void MainMenu()
    {
        ResumePause();
        SceneManager.LoadScene("MainMenu");
       
        Debug.Log("Spiel Reset");
    }
    public void GameEnded(int x, int y)
    {
        deactivate = true; // Sonst kamm nach den Tod wieder "entpausen"
    }
    public void QuitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Application.Quit();
    }
    
    private void OnDisable()
    {
        CodeEventHandler.GameEnded -= GameEnded;
    }
    
}
