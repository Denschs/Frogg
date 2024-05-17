using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAimGraphic : MonoBehaviour
{
    float charedMax;
    float startpos;
    bool fired;
    SpriteRenderer spriteRenderer;
    float spriteColorAlpha = 0;

    private void Start()
    {
        startpos = transform.position.x;
        spriteRenderer = GetComponent<SpriteRenderer>();


    }
    private void Update()
    {
        if (fired && (spriteColorAlpha >= 0))
        {
             spriteColorAlpha -= Time.deltaTime *255;
             spriteRenderer.color = new Color32(255, 255, 255, (byte)spriteColorAlpha);
        }
    }

    public void SetCharedMax(float charedMaxValue)
    {
        charedMax = charedMaxValue;
    }

    public void SetTargetPostion(float charging)
    {
        // Werte hier nach Augenmaﬂ
        transform.position = new Vector3(startpos + (15* (charging / charedMax)), transform.position.y, transform.position.z);
        
        if(charging < charedMax*0.15)
        {
            spriteColorAlpha = (byte)Mathf.Floor(255 * (charging / (charedMax * 0.15f)));
            spriteRenderer.color = new Color32(255, 255, 255, (byte)spriteColorAlpha);
        }
      
    }
    public void ToungeFired()
    {
        fired = true;
    }
    public void TongueCharging()
    {
        fired = false;
    }
}
