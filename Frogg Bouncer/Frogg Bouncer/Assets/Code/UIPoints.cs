using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIPoints : MonoBehaviour
{
    public TextMeshProUGUI foodedText; 
    public TextMeshProUGUI fairiesText;

    public TextMeshProUGUI foodedPanelText; 
    public TextMeshProUGUI fairiesPanelText;

    Vector2 punchVector = new Vector2(5f, 5f);

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
        DOTweenModuleUI.DOPunchAnchorPos(foodedText.rectTransform, punchVector,0.5f,20,10f, true);
        foodedText.text =  i.ToString();
        foodedPanelText.text = i.ToString();
            ;
    }

    private void UpdateFairiesCounter(int newCounter)
    {
        DOTweenModuleUI.DOPunchAnchorPos(fairiesText.rectTransform, punchVector, 0.5f, 0, 3f, true);
        fairiesText.text =newCounter.ToString();
        fairiesPanelText.text =newCounter.ToString();
    }
}
