using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public int targetFeeCount = 3; // Target number of "fee" game objects
    FrogPlayer player;

    private void Start()
    {
        player = FindAnyObjectByType<FrogPlayer>(); // Maybe Change later to c# Event 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
     
        // Check if the colliding object has the tag "fee"
        if (other.CompareTag("Schmetterling"))
        {
            CodeEventHandler.Trigger_LosingLife();
        }
        // Destroy the object
        Destroy(other.gameObject);

    }
}
