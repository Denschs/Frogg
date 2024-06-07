using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class UiHealth : MonoBehaviour
{
    List<RectTransform> gameObjects;
    void Start()
    {
        gameObjects = transform.GetComponentsInChildren<RectTransform>().ToList();
        CodeEventHandler.LosingLife += LoseHealtUi;      
    }

    private void OnDisable()
    {
        CodeEventHandler.LosingLife -= LoseHealtUi;
    }
    public void LoseHealtUi()
    {
        if (gameObjects.Count != 0)
        {
            RectTransform lastElement = gameObjects[gameObjects.Count - 1];

            // Animate the last element downwards
            lastElement.DOAnchorPosY(lastElement.anchoredPosition.y - 200f, 1f).SetEase(Ease.InOutQuad)
                .OnComplete(() => {
                    gameObjects.Remove(lastElement); // Entfernen des Elements aus der Liste nach der Animation
                });
        }
    }
}
