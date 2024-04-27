using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPoints : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
        CodeEventHandler.GettingPoints += UpdateScore;
        text = GetComponent<TextMeshProUGUI>(); 
    }

    private void OnDisable()
    {
        CodeEventHandler.GettingPoints -= UpdateScore;
    }
    private void UpdateScore(int i)
    {
        text.text = "Points " + i;
    }
}
