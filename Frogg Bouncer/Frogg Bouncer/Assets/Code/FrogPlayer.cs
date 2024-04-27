using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrogPlayer : MonoBehaviour
{
    AttackZone attackZone;
    bool cooldown = false;
    GameObject tounge;
    [SerializeField] int hp = 3;
    [SerializeField] int score = 0;
    void Start()
    {
        attackZone = FindAnyObjectByType<AttackZone>();
        tounge = transform.GetChild(0).gameObject;
        CodeEventHandler.LosingLife += LoseLife;
    }

    void OnTounge() // Because of Send Message in Player Input this Method will be called, maybe change later to Unity Events
    {
        if (!cooldown)
        {
            Tuple<Vector3, EnemyType> attaked = attackZone.ToungeInZone();
            if(attaked != null)
            {
                tounge.transform.right = attaked.Item1 - tounge.transform.position;
                tounge.transform.localScale = new Vector3(Vector2.Distance(attaked.Item1 , tounge.transform.position) *1.1f,1,1);
                print(attaked.Item2);
                if(attaked.Item2 == EnemyType.Fairy)
                {
                    CodeEventHandler.Trigger_LosingLife();
                }
                else
                {
                    score++;
                    CodeEventHandler.Trigger_GettingPoints(score);
                }
            }
            StartCoroutine(CoolDown());
        }  
    }
    IEnumerator CoolDown()
    {
        cooldown = true;
        yield return new WaitForSeconds(0.25f);
        for (int i = 5; i > 1; i--)
        {
            tounge.transform.localScale = new Vector3(tounge.transform.localScale.x * 0.5f, 1, 1);
            yield return new WaitForSeconds(0.05f);
        }
        tounge.transform.localScale = new Vector3(0, 1, 1);
        cooldown = false;
    }
    public void LoseLife()
    {
        hp--;
        if(hp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void OnDisable()
    {
        CodeEventHandler.LosingLife -= LoseLife;
    }
}
