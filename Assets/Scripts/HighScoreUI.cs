using UnityEngine;
using TMPro;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText; // Drag your TMP_Text here

    void Start()
    {
        UpdateHighScoreUI();
    }

    // Call this whenever the high score changes
    public void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            int highScore = HighScoreManager.GetHighScore();
            highScoreText.text = "High Score: " + highScore;
        }
    }
}