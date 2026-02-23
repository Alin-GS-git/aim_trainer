using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("End Panel Stats")]
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] TMP_Text finalMissesText;
    [SerializeField] TMP_Text finalAccuracyText;
    [SerializeField] TMP_Text finalHighScoreText; // Added for High Score display

    void OnEnable()
    {
        Timer.OnGameEnded += OnGameEnded;
    }

    void OnDisable()
    {
        Timer.OnGameEnded -= OnGameEnded;
    }

    void OnGameEnded()
    {
        // Get the stats
        int hits = ScoreCounter.Score;
        int misses = MissCounter.Misses;
        int total = hits + misses;
        float acc = total > 0 ? (float)hits / total * 100f : 0f;

        // Update end panel UI
        if (finalScoreText != null) finalScoreText.text = $"{hits}";
        if (finalMissesText != null) finalMissesText.text = $"{misses}";
        if (finalAccuracyText != null) finalAccuracyText.text = $"{acc:0}%";

        // Save new high score if it's higher
        HighScoreManager.SaveScore(hits); // Or your final score calculation

        // Display the high score on the UI
        if (finalHighScoreText != null)
        {
            int highScore = HighScoreManager.GetHighScore();
            finalHighScoreText.text = $"High Score: {highScore}";
        }
    }
}