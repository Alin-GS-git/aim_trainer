using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Camera playerCamera;

    void Update()
    {
        if (MovingTimer.GameEnded)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                TargetMover target = hit.collider.GetComponent<TargetMover>();

                if (target != null && target.gameObject.activeSelf)
                {
                    // Hit a target
                    MovingScoreCounter.AddScore();
                    target.gameObject.SetActive(false); // deactivate target
                }
                else
                {
                    // Missed shot
                    MovingScoreCounter.AddMiss();
                }
            }
            else
            {
                // Raycast hit nothing → also count as a miss
                MovingScoreCounter.AddMiss();
            }
        }
    }
}