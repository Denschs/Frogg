using UnityEngine;

public enum EnemyType
{
    Butterfly,
    FairyElectric,
    FairyIce,
    FairyFire,
    None
}
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] EnemyType enemyType;
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
    public EnemyType GetEnemyType()
    {
        return enemyType;
    }
}
