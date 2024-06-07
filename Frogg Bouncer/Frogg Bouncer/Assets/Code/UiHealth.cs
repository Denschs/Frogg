using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class UiHealth : MonoBehaviour
{
    List<Transform> gameObjects;
    void Start()
    {
        gameObjects = transform.GetComponentsInChildren<Transform>().ToList();
        CodeEventHandler.LosingLife += LoseHealtUi;      
    }

    private void OnDisable()
    {
        CodeEventHandler.LosingLife -= LoseHealtUi;
    }
    public void LoseHealtUi()
    {
        if(gameObjects.Count != 0)
        {
           
            gameObjects[gameObjects.Count - 1].gameObject.SetActive(false);
            //DOTweenModuleUI.DOAnchorPosY(gameObjects[gameObjects.Count - 1].gameObjec.transform, 5, 1f,true);
            gameObjects.RemoveAt(gameObjects.Count - 1);
        }
    }
}
