using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Bilder;

    [SerializeField]
    private string Scenename;

    public bool nextScene = false;

    private int introIndex = -1;

    public GameObject mainMenu;

    void Start()
    {
        NextFrame();
    }

    void Update()
    {
        if(Input.anyKeyDown && introIndex < Bilder.Length)
        {
            NextFrame();
        }
    }

    public void NextFrame()
    {
        introIndex++;

        if(introIndex < Bilder.Length)
        {
            Bilder[introIndex].SetActive(true);

            Bilder[introIndex].GetComponent<FadeOnClick>().FadeIn();

            Bilder[introIndex].GetComponent<FadeOnClick>().AfterFadeIn();
        }


        if(introIndex >= Bilder.Length && nextScene == true)
        {
            SceneManager.LoadScene(Scenename);
            this.gameObject.SetActive(false);
            //this.gameObject.SetActive(false);
            //mainMenu.SetActive(true);
            return;
        }
        if(introIndex >= Bilder.Length && nextScene == false)
        {
            //Application.Quit();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            this.gameObject.SetActive(false);
            //GameManager.Instance.ToggleTimeRunning(true);
            Debug.Log("Intro Over");
            //mainMenu.SetActive(true);

        }
    }
}