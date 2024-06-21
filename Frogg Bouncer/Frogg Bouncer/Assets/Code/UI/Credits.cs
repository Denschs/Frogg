using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sevensindemon
{
    public class Credits : MonoBehaviour
    {
        public RectTransform floatingText;
        public float speed;
       
        // Lässt den Text automatsich ablaufen
        void Update()
        {
            floatingText.anchoredPosition +=  new Vector2(0f, speed);
        }
    }
}
