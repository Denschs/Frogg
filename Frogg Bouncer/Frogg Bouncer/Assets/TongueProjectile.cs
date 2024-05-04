using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueProjectile : MonoBehaviour
{
    Rigidbody2D rigi;
    // Start is called before the first frame update
    void Awake()
    {
        rigi = GetComponent<Rigidbody2D>(); 
    }
    public void Push(Vector2 dir,float strength)
    {
        rigi.velocity = dir* strength;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        EnemyType typeOfEnemiy = collision.gameObject.GetComponent<EnemyMovement>().GetEnemyType();

        
        print(typeOfEnemiy);
        if (typeOfEnemiy == EnemyType.Fairy)
        {
            CodeEventHandler.Trigger_LosingLife();
        }
        else
        {
          
            CodeEventHandler.Trigger_GettingPointsRaw();
        }
        Destroy(collision.gameObject);
    }


}
