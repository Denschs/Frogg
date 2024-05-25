using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;


public class FrogPlayer : MonoBehaviour
{
    AttackZone attackZone;
    [SerializeField] bool cooldown = false;
    GameObject tounge;
    [SerializeField] int hp = 3;
    [SerializeField] int score = 0;
    [SerializeField] GameObject proj;
    bool pressed;
    [SerializeField] bool reversed;
    public float charedvalue;
    [SerializeField] float charedvalueMaxSec = 2;

    [SerializeField] SpriteRenderer spriteRendererIndiactor;
    [SerializeField] GameObject endScreen;

    private bool canShootTongue = true; 
    public float electricDebuffTime;
    public float iceDebuffTime;
    public float fireDebuffTime;

    public float iceDebuffValue;
    public float fireDebuffValue;

    bool isIce;
    bool isFire;

    HighscoreManager highscoreManager;
    DeathZone deathZone;
    TargetAimGraphic trgetAimGraphic;

    private AudioSource audioSource;
    public AudioClip tongueClip;
    public AudioClip butterflyPassClip;
    public AudioClip electroClip;
    public AudioClip iceClip;
    public AudioClip fireClip;
    public AudioClip gulbClip;

    [SerializeField] GameObject toungeTargetPoint;
    Animator animator;

    void Start()
    {
        attackZone = FindAnyObjectByType<AttackZone>();
        tounge = transform.GetChild(0).gameObject;

        deathZone = FindObjectOfType<DeathZone>();
        highscoreManager = FindObjectOfType<HighscoreManager>();

        CodeEventHandler.LosingLife += LoseLife;
        CodeEventHandler.GettingPointsRaw += GettingHit;
        CodeEventHandler.ElectricDebufStarter += ElectricDebuffStarter;
        CodeEventHandler.IceDebuffStarter += IceDebuffStarter;
        CodeEventHandler.FireDebuffStarter += FireDebuffStarter;
        CodeEventHandler.ToungeIsBack += CoolDownEnd;

        audioSource = GetComponentInChildren<AudioSource>();

        trgetAimGraphic = FindAnyObjectByType<TargetAimGraphic>();
        trgetAimGraphic?.SetCharedMax(charedvalueMaxSec,reversed);

        charedvalue = ((!reversed) ? 0 : charedvalueMaxSec);

        animator = GetComponentInChildren<Animator>();

    }
    private void Update()
    {
        if (pressed)
        {
            if(isIce)
            {
                charedvalue += Time.deltaTime / iceDebuffValue * ((!reversed) ? 1 : -1);
            }
            else if(isFire)
            {
                charedvalue += Time.deltaTime * fireDebuffValue * ((!reversed) ? 1 : -1); 
            }
            else
            {
                charedvalue += Time.deltaTime * ((!reversed) ? 1 : -1); 
            }

            if (charedvalue <= charedvalueMaxSec / 2) //can propaly done easeyer
            {
                spriteRendererIndiactor.color = Color.Lerp(Color.green, Color.yellow, charedvalue / (charedvalueMaxSec / 2));
            }
            else
            {
                spriteRendererIndiactor.color = Color.Lerp(Color.yellow, Color.red, charedvalue - (charedvalueMaxSec / 2) / (charedvalueMaxSec / 2));
            }


            if ((!reversed) ? (charedvalue > charedvalueMaxSec) : (charedvalue <= 0))
            {
                charedvalue = (!reversed) ? charedvalueMaxSec : 0;
            }

            trgetAimGraphic?.SetTargetPostion(charedvalue);

        }
    }


    public void Holding(InputAction.CallbackContext context)
    {

        if (context.started && canShootTongue)
        {
            pressed = true;
            trgetAimGraphic?.TongueCharging();

        }
        else if (context.canceled && canShootTongue)
        {
            if (!cooldown)
            {
                animator.SetTrigger("Attack");
                GameObject newproj = Instantiate(proj, toungeTargetPoint.gameObject.transform.position, Quaternion.identity);
                newproj.GetComponent<TongueProjectile>().Push(new Vector2(1, 0), 6.75f * charedvalue + 1);
                charedvalue = (!reversed) ? 0 : charedvalueMaxSec;
                audioSource.clip = tongueClip;
                audioSource.Play();
                cooldown = true;
            }
            pressed = false;
            trgetAimGraphic?.ToungeFired();


        }
    }
    void CoolDownEnd(bool gotSomethingToEat)
    {
        if (canShootTongue)
        {
            print("CoolDownEnd");
            if (gotSomethingToEat)
            {
                animator.SetTrigger("Eat");
            }
            else
            {
                print("Just Idle");
                animator.SetTrigger("Idle");
            }
            
        }
        cooldown = false;
    }


    void OnTounge() 
    {
        if (!cooldown)
        {
            OldTargetTounge();
            StartCoroutine(CoolDown());
        }
    }

    private void OldTargetTounge()
    {
        Tuple<Vector3, EnemyType> attacked = attackZone.ToungeInZone();
        if (attacked != null)
        {
            tounge.transform.right = attacked.Item1 - tounge.transform.position;
            tounge.transform.localScale = new Vector3(Vector2.Distance(attacked.Item1, tounge.transform.position) * 1.1f, 1, 1);
            print(attacked.Item2);
        }
    }

    public void ElectricDebuffStarter()
    {
        animator.SetBool("Zap",true);
        StartCoroutine(ElectricDebuff());
    }

    public void IceDebuffStarter()
    {
        animator.SetBool("Ice", true);
        StartCoroutine(IceDebuff());
    }

    public void FireDebuffStarter()
    {
        animator.SetBool("Fire", true);
        StartCoroutine(FireDebuff());
    }

    public IEnumerator ElectricDebuff()
    {
        print("Electric debuff applied");
        audioSource.clip = electroClip;
        audioSource.Play();
        canShootTongue = false;
        yield return new WaitForSeconds(electricDebuffTime);
        canShootTongue = true;
        animator.SetBool("Zap", false);
    }

    public IEnumerator IceDebuff()
    {
        print("Ice debuff applied");
        audioSource.clip = iceClip;
        audioSource.Play();     
        isIce = true;
        yield return new WaitForSeconds(iceDebuffTime);
        isIce = false;
        animator.SetBool("Ice", false);
    }

    public IEnumerator FireDebuff()
    {
        print("Fire debuff applied");
        audioSource.clip = fireClip;
        audioSource.Play();
        isFire = true;
        yield return new WaitForSeconds(fireDebuffTime);
        isFire = false; 
        animator.SetBool("Fire", false);
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
        audioSource.clip = butterflyPassClip;
        audioSource.Play();
        hp--;
        if (hp <= 0)
        {
            highscoreManager.SaveHighscores(score, deathZone.fairiescounter);
            endScreen.SetActive(true);
        }
    }
    public void GettingHit()
    {
        //audioSource.clip = gulbClip; //[UnityEngine.Random.Range(0, gulbClip.Length)]
        AudioSource.PlayClipAtPoint(gulbClip, Camera.main.transform.position);
        score++;
        CodeEventHandler.Trigger_GettingPoints(score);
    }

    
    private void OnDisable()
    {
        CodeEventHandler.LosingLife -= LoseLife;
        CodeEventHandler.GettingPointsRaw -= GettingHit;
        CodeEventHandler.ElectricDebufStarter -= ElectricDebuffStarter;
        CodeEventHandler.IceDebuffStarter -= IceDebuffStarter;
        CodeEventHandler.FireDebuffStarter -= FireDebuffStarter;
        CodeEventHandler.ToungeIsBack -= CoolDownEnd;
    }
}
