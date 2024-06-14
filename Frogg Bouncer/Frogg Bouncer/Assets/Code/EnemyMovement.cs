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
    [SerializeField] float accelerationValue = 0.8f;
    [SerializeField] float accelerationValueMutiple = 1f;
    public float speed = 3f;

    private void Update()
    {
        MoveLeft();
    }

    private void MoveLeft()
    {
        // Bewege den Feind nach links basierend auf der Geschwindigkeit
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        speed += Time.deltaTime* (accelerationValue* accelerationValueMutiple);

    }
    public EnemyType GetEnemyType()
    {
        return enemyType;
    }
    public void SetaccelerationValue(float newaccelerationValue)
    {
          accelerationValue = newaccelerationValue;
    }
    public void SetaccelerationValueMutiple(float newMutiple)
    {
        accelerationValueMutiple = newMutiple;
    }
}
