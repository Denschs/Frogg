using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueProjectile : MonoBehaviour
{
    Rigidbody2D rigi;
    [SerializeField] bool hit = false;
    LineRenderer lineRenderer;
    TrailRenderer trailRenderer;
    Vector3 oringP;
    FrogPlayer frogPlayer;

    void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        frogPlayer = FindObjectOfType<FrogPlayer>();
    }
    private void Update()
    {
        if (hit)
        {
            Vector3[] points = new[] { oringP, transform.position };
            lineRenderer.SetPositions(points);
            
            if(Vector2.Distance(transform.position, oringP) < 0.01f)
            {
                Destroy(gameObject);
            }
        }
    }
    private void Start()
    {
        oringP = transform.position;
    }
    public void Push(Vector2 dir,float strength)
    {
        rigi.velocity = dir* strength;
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

            EnemyMovement enemyMovement = collision.gameObject.GetComponent<EnemyMovement>();

            if (enemyMovement != null)
            {
                EnemyType typeOfEnemy = enemyMovement.GetEnemyType();

                switch (typeOfEnemy)
                {
                    case EnemyType.FairyElectric:
                        StartCoroutine(frogPlayer.ElectricDebuff());
                        break;
                    case EnemyType.FairyIce:
                        StartCoroutine(frogPlayer.IceDebuff());
                        break;
                    case EnemyType.FairyFire:
                        StartCoroutine(frogPlayer.FireDebuff());
                        break;
                    case EnemyType.Butterfly:
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


}
