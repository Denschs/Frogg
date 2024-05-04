using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    private float elapsedTime = 0f;
    public TextMeshProUGUI timeText;

    void Update()
    {
        // Add the time passed since the last frame to the elapsed time
        elapsedTime += Time.deltaTime;

        // Display elapsed time using TextMeshPro
        if (timeText != null)
        {
            timeText.text = "Time: " + elapsedTime.ToString("F0");
        }
    }
}
