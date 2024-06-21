using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOnClick : MonoBehaviour
{
    public Animator animator;

    
    public bool isFading = false;

    
    public void FadeIn()
    {
        isFading = true;
        animator.Play("FadeIn");
        
    }

    
    public void FadeOut()
    {
        isFading = true;
        animator.SetBool("stopFade", false);
        AfterFadeOut();
        
    }

    
    public void AfterFadeIn()
    {
        isFading = false;
    }
    
    public void AfterFadeOut()
    {
        isFading = false;
        FindObjectOfType<IntroManager>().NextFrame();
        gameObject.SetActive(false);
    }
}
    

