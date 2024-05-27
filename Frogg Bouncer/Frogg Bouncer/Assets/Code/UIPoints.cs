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
        foodedText.text =  i.ToString();
        foodedPanelText.text = i.ToString();
            ;
    }

    private void UpdateFairiesCounter(int newCounter)
    {
        fairiesText.text =newCounter.ToString();
        fairiesPanelText.text =newCounter.ToString();
    }
}
