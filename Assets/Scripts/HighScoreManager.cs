using UnityEngine;

public static class HighScoreManager
{
    private const string HighScoreKey = "HighScore";

    // Call this to save a new score
    public static void SaveScore(int score)
    {
        // Get the current saved high score (default 0)
        int currentHigh = PlayerPrefs.GetInt(HighScoreKey, 0);

        // Only save if the new score is higher
        if (score > currentHigh)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
            PlayerPrefs.Save(); // Make sure it writes to disk
            Debug.Log("New High Score Saved: " + score);
        }
    }

    // Call this to retrieve the saved high score
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }
}