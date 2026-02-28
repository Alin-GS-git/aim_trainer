using TMPro;
using UnityEngine;

public class MovingScoreCounter : MonoBehaviour
{
    public static int score = 0;
    public static int misses = 0;

    [Header("UI References")]
    public TMP_Text scoreText;   // Drag your Score TMP-Text here
    public TMP_Text missesText;  // Drag your Misses TMP-Text here

    void Start()
    {
        score = 0;
        misses = 0;
        UpdateUI();
    }

    public static void AddScore()
    {
        if (!MovingTimer.GameEnded)
        {
            score++;
            FindAnyObjectByType<MovingScoreCounter>().UpdateUI();
        }
    }

    public static void AddMiss()
    {
        if (!MovingTimer.GameEnded)
        {
            misses++;
            FindAnyObjectByType<MovingScoreCounter>().UpdateUI();
        }
    }

    public void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (missesText != null)
            missesText.text = "Misses: " + misses;
    }
}