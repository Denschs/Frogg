using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseButtons : MonoBehaviour
{
    public void OnReplayButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
