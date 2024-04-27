using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public int targetFeeCount = 3; // Target number of "fee" game objects

    private void OnTriggerEnter2D(Collider2D other)
    {
     
        // Check if the colliding object has the tag "fee"
        if (other.CompareTag("Fee"))
        {
            targetFeeCount--; // Increment the count of "fee" game objects

            // Check if the target count has been reached
            if (targetFeeCount <= 0)
            {
                // Reload the level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        // Destroy the object
        Destroy(other.gameObject);

    }
}
