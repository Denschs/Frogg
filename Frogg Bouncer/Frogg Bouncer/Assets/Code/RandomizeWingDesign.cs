using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeWingDesign : MonoBehaviour
{
    SpriteRenderer fullwingSprite;
    SpriteRenderer wingDesignSprite;
    [SerializeField] Sprite[] wingsdesign;

    void Start()
    {
        fullwingSprite = GetComponent<SpriteRenderer>();
        wingDesignSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        
        fullwingSprite.color = GetRandomColor();
        wingDesignSprite.color = GetRandomColor();

        wingDesignSprite.sprite = wingsdesign[Random.Range(0, wingsdesign.Length)];

    }

    private Color32 GetRandomColor()
    {
        return new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
    }
}
