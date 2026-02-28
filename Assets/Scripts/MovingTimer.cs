using TMPro;
using UnityEngine;

public class MovingTimer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] float gameTime = 10f; // total time in seconds

    public static bool GameEnded { get; private set; }

    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;

    private float timeLeft;

    void Start()
    {
        timeLeft = gameTime;
        GameEnded = false;

        // Hide panel at start
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateTimerUI();
    }

    void Update()
    {
        if (GameEnded) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            GameEnded = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);

            if (finalScoreText != null)
                finalScoreText.text = "Final Score: " + MovingScoreCounter.score;
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = $"Time: {Mathf.CeilToInt(timeLeft)}";
    }
}