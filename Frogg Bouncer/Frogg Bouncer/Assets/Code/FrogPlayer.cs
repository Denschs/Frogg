using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using DG.Tweening;


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
    [SerializeField] GameObject scoreCanvas;

    public float electricDebuffTime;
    public float electricduration = 0.5f;
    public float electricstrength = 0.5f;

    public float iceDebuffTime;
    public float iceduration = 0.5f;
    public float icestrength = 0.5f;

    public float fireDebuffTime;
    public float fireduration = 0.5f;
    public float firestrength = 0.5f;

    public float iceDebuffValue;
    public float fireDebuffValue;

    private bool canShootTongue = true; 
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
    [SerializeField] ParticleEffectManager particleEffectManager;

    private RectTransform endScreenCanvasGroup;
    [SerializeField] private RectTransform fairyScoreCanvas;
    [SerializeField] private RectTransform butterflyScoreCanvas;

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
        animator = GetComponentInChildren<Animator>();
        trgetAimGraphic = FindAnyObjectByType<TargetAimGraphic>();
        trgetAimGraphic?.SetCharedMax(charedvalueMaxSec,reversed);

        charedvalue = ((!reversed) ? 0 : charedvalueMaxSec);

        endScreenCanvasGroup = endScreen.GetComponent<RectTransform>();
        if (endScreenCanvasGroup == null)
        {
            endScreenCanvasGroup = endScreen.AddComponent<RectTransform>();
        }

        //fairyScoreCanvas = scoreCanvas.GetComponent<RectTransform>();
        //butterflyScoreCanvas = scoreCanvas.GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (pressed & canShootTongue)
        {
            if (isIce)
            {
                charedvalue += Time.deltaTime / iceDebuffValue * ((!reversed) ? 1 : -1);
            }
            else if (isFire)
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

        if (hp <= 0)
        {
            charedvalue = 0;
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
        FastTargetFadeOut();
        StartCoroutine(ElectricDebuff());
    }

    public void IceDebuffStarter()
    {
        animator.SetBool("Ice", true);
        FastTargetFadeOut();
        StartCoroutine(IceDebuff());
    }

    public void FireDebuffStarter()
    {
        animator.SetBool("Fire", true);
        FastTargetFadeOut();
        StartCoroutine(FireDebuff());
    }

    public IEnumerator ElectricDebuff()
    {
        CameraShake.Shake(electricduration, electricstrength);
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
        CameraShake.Shake(iceduration, icestrength);
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
        CameraShake.Shake(fireduration, firestrength);
        print("Fire debuff applied");
        audioSource.clip = fireClip;
        audioSource.Play();
        isFire = true;
        particleEffectManager.ActivateParticleSystem(0);
        yield return new WaitForSeconds(fireDebuffTime);
        particleEffectManager.DeactivateParticleSystem(0);
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
            CodeEventHandler.Trigger_GameEnded(score, deathZone.fairiescounter);
            endScreen.SetActive(true);
            //endScreenCanvasGroup.DOFade(1f, 1f).From(0f).SetEase(Ease.InOutQuad);
            endScreenCanvasGroup.DOAnchorPosY(-600f, 1.5f).SetEase(Ease.InOutQuad);

            //scoreCanvasGroup.DOFade(0f, 1f).OnComplete(() =>
            fairyScoreCanvas.DOAnchorPosY(100f, 1f).SetEase(Ease.InOutQuad);
            butterflyScoreCanvas.DOAnchorPosY(100f, 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                scoreCanvas.SetActive(false); // Deaktivieren nach dem Fade-Out
                //Time.timeScale = 0;
            });
        }
    }
    public void GettingHit()
    {

        audioSource.clip = gulbClip; //[UnityEngine.Random.Range(0, gulbClip.Length)]
        audioSource.Play();
        score++;
        CodeEventHandler.Trigger_GettingPoints(score);
        FastTargetFadeOut();

    }

    private void FastTargetFadeOut()
    {
        if (charedvalue <= 0)
        {
            StartCoroutine(trgetAimGraphic.FastFadeOut());
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        CodeEventHandler.LosingLife -= LoseLife;
        CodeEventHandler.GettingPointsRaw -= GettingHit;
        CodeEventHandler.ElectricDebufStarter -= ElectricDebuffStarter;
        CodeEventHandler.IceDebuffStarter -= IceDebuffStarter;
        CodeEventHandler.FireDebuffStarter -= FireDebuffStarter;
        CodeEventHandler.ToungeIsBack -= CoolDownEnd;
    }
}
