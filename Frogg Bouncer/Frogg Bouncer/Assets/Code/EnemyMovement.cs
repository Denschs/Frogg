using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;

    private void Update()
    {
        MoveLeft();
    }

    private void MoveLeft()
    {
        // Bewege den Feind nach links basierend auf der Geschwindigkeit
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
