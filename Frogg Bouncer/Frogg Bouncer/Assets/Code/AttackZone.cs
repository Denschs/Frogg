using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField] GameObject inZone;
    private void OnTriggerEnter2D(Collider2D collision)
    {             
       inZone = collision.gameObject;            
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inZone = null;
    }
    public Tuple<Vector3, EnemyType> ToungeInZone()
    {
        if(inZone != null)
        {
            Destroy(inZone, 0.1f);
            return Tuple.Create(inZone.transform.position, inZone.gameObject.GetComponent<EnemyMovement>().GetEnemyType());
        }
        return null;
     
    }
}
