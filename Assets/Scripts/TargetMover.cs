using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public float baseSpeed = 3f;        // Starting speed
    public float speedMultiplier = 2f;  // How aggressive speed increase is
    public float moveRange = 8f;

    private Vector3 startPosition;
    private Vector3 moveDirection;
    private bool isHit = false;

    void Start()
    {
        startPosition = transform.position;

        moveDirection = new Vector3(
            Random.Range(-1f, 1f),
            0f,
            Random.Range(-1f, 1f)
        ).normalized;
    }

    void Update()
    {
        if (MovingTimer.GameEnded || isHit)
            return;

        float currentSpeed = baseSpeed;

        // After 5 seconds, speed increases rapidly
        if (Time.timeSinceLevelLoad > 5f)
        {
            float extraTime = Time.timeSinceLevelLoad - 5f;
            currentSpeed += extraTime * speedMultiplier;
        }

        transform.position += moveDirection * currentSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, startPosition) > moveRange)
        {
            moveDirection = new Vector3(
                Random.Range(-1f, 1f),
                0f,
                Random.Range(-1f, 1f)
            ).normalized;
        }
    }

    public void Hit()
    {
        if (isHit) return;

        isHit = true;
        MovingScoreCounter.AddScore();
        gameObject.SetActive(false);
    }
}