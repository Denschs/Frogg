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
    public float charedvalue;
    float charedvalueMaxSec = 2;
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

    [SerializeField] TextMeshProUGUI textMeshProUGUI; // Just for PlaceHolder Feedback

    private AudioSource audioSource;
    public AudioClip tongueClip;
    public AudioClip butterflyPassClip;
    public AudioClip electroClip;
    public AudioClip iceClip;
    public AudioClip fireClip;
    public AudioClip[] gulbClip;

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

    }
    private void Update()
    {
        if (pressed)
        {
            if(isIce)
            {
                charedvalue += Time.deltaTime / iceDebuffValue;
            }
            else if(isFire)
            {
                charedvalue += Time.deltaTime * fireDebuffValue;
            }
            else
            {
                charedvalue += Time.deltaTime;
            }

            if (charedvalue <= charedvalueMaxSec / 2) //can propaly done easeyer
            {
                spriteRendererIndiactor.color = Color.Lerp(Color.green, Color.yellow, charedvalue / (charedvalueMaxSec / 2));
            }
            else
            {
                spriteRendererIndiactor.color = Color.Lerp(Color.yellow, Color.red, charedvalue - (charedvalueMaxSec / 2) / (charedvalueMaxSec / 2));
            }


            if (charedvalue > charedvalueMaxSec)
            {
                charedvalue = charedvalueMaxSec;
            }

        }
    }


    public void Holding(InputAction.CallbackContext context)
    {

        if (context.started && canShootTongue)
        {
            pressed = true;
           
        }
        else if (context.canceled && canShootTongue)
        {
            if (!cooldown)
            {
                GameObject newproj = Instantiate(proj, transform.position, Quaternion.identity);
                newproj.GetComponent<TongueProjectile>().Push(new Vector2(1, 0), 6 * charedvalue + 1);
                charedvalue = 0;
                audioSource.clip = tongueClip;
                audioSource.Play();
                cooldown = true;
            }
            pressed = false;


        }
    }
    void CoolDownEnd()
    {
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
        StartCoroutine(ElectricDebuff());
    }

    public void IceDebuffStarter()
    {
        StartCoroutine(IceDebuff());
    }

    public void FireDebuffStarter()
    {
        StartCoroutine(FireDebuff());
    }

    public IEnumerator ElectricDebuff()
    {
        print("Electric debuff applied");
        audioSource.clip = electroClip;
        audioSource.Play();
        textMeshProUGUI.text = "Thats Shocking";
        textMeshProUGUI.color = Color.yellow;
        canShootTongue = false;
        yield return new WaitForSeconds(electricDebuffTime);
        canShootTongue = true; 
        print("Electric debuff removed");

        textMeshProUGUI.text = "";
    }

    public IEnumerator IceDebuff()
    {
        print("Ice debuff applied");
        audioSource.clip = iceClip;
        audioSource.Play();
        textMeshProUGUI.text = "Cold feet";
        textMeshProUGUI.color = Color.blue;
        isIce = true;
        yield return new WaitForSeconds(iceDebuffTime);
        isIce = false;
        print("Ice debuff removed");

        textMeshProUGUI.text = "";
    }

    public IEnumerator FireDebuff()
    {
        print("Fire debuff applied");
        audioSource.clip = fireClip;
        audioSource.Play();
        textMeshProUGUI.text = "This Frog is on Fire";
        textMeshProUGUI.color = Color.red;
        isFire = true;
        yield return new WaitForSeconds(fireDebuffTime);
        isFire = false; 
        print("Fire debuff removed");

        textMeshProUGUI.text = "";
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
        audioSource.clip = gulbClip[UnityEngine.Random.Range(0, gulbClip.Length)];
        audioSource.Play();
        score++;
        CodeEventHandler.Trigger_GettingPoints(score);
    }

    
    private void OnDisable()
    {
        CodeEventHandler.LosingLife -= LoseLife;
        CodeEventHandler.ElectricDebufStarter -= ElectricDebuffStarter;
        CodeEventHandler.IceDebuffStarter -= IceDebuffStarter;
        CodeEventHandler.FireDebuffStarter -= FireDebuffStarter;
    }
}
