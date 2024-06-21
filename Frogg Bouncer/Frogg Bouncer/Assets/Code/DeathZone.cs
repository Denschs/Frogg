using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public int fairiescounter;
    FrogPlayer player;

    private void Start()
    {
        player = FindAnyObjectByType<FrogPlayer>(); // Maybe Change later to c# Event 
        CodeEventHandler.GameEnded += DestroyDeathzone;
    }

    void DestroyDeathzone(int fairiesGuestsScore, int butterfliesFedScore)
    {
        Destroy(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "Schmetterling"
        if (other.CompareTag("Schmetterling"))
        {
            CodeEventHandler.Trigger_LosingLife();
            // Destroy the object
            Destroy(other.gameObject);
        }

        // Check if the colliding object has the tag "Fee"
        if (other.CompareTag("Fee"))
        {
            fairiescounter++;
            CodeEventHandler.Trigger_FairyCounterChanged(fairiescounter); // Trigger the event
                                                                         
            Destroy(other.gameObject);
        }

        // Destroy the object
        Destroy(other.gameObject);
    }
}
