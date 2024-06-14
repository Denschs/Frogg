using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeWingDesign : MonoBehaviour
{
    SpriteRenderer fullwingSprite;
    SpriteRenderer wingDesignSprite;
    [SerializeField] Sprite[] wingsdesign;
    [SerializeField] Color32[] wingsColors;

    void Start()
    {
        fullwingSprite = GetComponent<SpriteRenderer>();
        wingDesignSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        
        fullwingSprite.color = GetRandomColorFromList();
        wingDesignSprite.color = GetRandomColorFromList();

        wingDesignSprite.sprite = wingsdesign[Random.Range(0, wingsdesign.Length)];

    }

    private Color32 GetRandomColor()
    {
        return new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
    }
    private Color32 GetRandomColorFromList()
    {
        return wingsColors[Random.Range(0, wingsColors.Length)];
    }
}
