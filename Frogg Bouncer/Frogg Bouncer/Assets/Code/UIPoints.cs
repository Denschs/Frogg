using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPoints : MonoBehaviour
{
    public TextMeshProUGUI foodedText; 
    public TextMeshProUGUI fairiesText;

    public TextMeshProUGUI foodedPanelText; 
    public TextMeshProUGUI fairiesPanelText; 

    void Start()
    {
        CodeEventHandler.GettingPoints += UpdateScore;
        CodeEventHandler.FairyCounterChanged += UpdateFairiesCounter;
    }

    private void OnDisable()
    {
        CodeEventHandler.GettingPoints -= UpdateScore;
        CodeEventHandler.FairyCounterChanged -= UpdateFairiesCounter;
    }

    private void UpdateScore(int i)
    {
        foodedText.text = "Fooded: " + i;
        foodedPanelText.text = "Fooded: " + i;
    }

    private void UpdateFairiesCounter(int newCounter)
    {
        fairiesText.text = "Guests: " + newCounter;
        fairiesPanelText.text = "Guests: " + newCounter;
    }
}
