using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueProjectile : MonoBehaviour
{
    Rigidbody2D rigi;
    [SerializeField] bool hit = false;
    [SerializeField] float extraSpeedFactor = 1.5f;
    LineRenderer lineRenderer;
    TrailRenderer trailRenderer;
    Vector3 oringP;
    FrogPlayer frogPlayer;


    public AudioClip fairyGulbClip;
    public AudioClip butterflyGulbClip;

    void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        frogPlayer = FindObjectOfType<FrogPlayer>();

        rigi.gravityScale = Mathf.Pow(extraSpeedFactor, 2);
    }
    private void Update()
    {
        if (hit)
        {
            Vector3[] points = new[] { oringP, transform.position };
            lineRenderer.SetPositions(points);                    
        }
    }
    private void Start()
    {
        oringP = transform.position;
    }
    public void Push(Vector2 dir,float strength)
    {
        rigi.velocity = dir* strength * extraSpeedFactor;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hit)
        {
            if (collision.CompareTag("MissedTarget"))
            {
                hit = true;
                trailRenderer.widthMultiplier = 0.0f;
                rigi.gravityScale = 0.0f;
                Push(oringP - transform.position, 5);
                return;
            }
            else if (!collision.CompareTag("Player"))
            {
                EnemyMovement enemyMovement = collision.gameObject.GetComponent<EnemyMovement>();

                if (enemyMovement != null)
                {
                    EnemyType typeOfEnemy = enemyMovement.GetEnemyType();

                    switch (typeOfEnemy)
                    {
                        case EnemyType.FairyElectric:
                            //AudioSource.PlayClipAtPoint(fairyGulbClip, Camera.main.transform.position);
                            CodeEventHandler.Trigger_ElectricDebufStarter();
                            break;
                        case EnemyType.FairyIce:
                            AudioSource.PlayClipAtPoint(fairyGulbClip, Camera.main.transform.position);
                            CodeEventHandler.Trigger_IceDebuffStarter();
                            break;
                        case EnemyType.FairyFire:
                            AudioSource.PlayClipAtPoint(fairyGulbClip, Camera.main.transform.position);
                            CodeEventHandler.Trigger_FireDebuffStarter();
                            break;
                        case EnemyType.Butterfly:
                            AudioSource.PlayClipAtPoint(butterflyGulbClip, Camera.main.transform.position);
                            CodeEventHandler.Trigger_GettingPointsRaw();
                            break;
                    }
                }

                Destroy(collision.gameObject);
                hit = true;
                trailRenderer.widthMultiplier = 0.0f;
                rigi.gravityScale = 0.0f;
                Push(oringP - transform.position, 5);
            }

            
        }
        else
        {        
            if (collision.CompareTag("Player"))
            {         
                CodeEventHandler.Trigger_ToungeIsBack();
                Destroy(gameObject);
            }
        }
    }


}
