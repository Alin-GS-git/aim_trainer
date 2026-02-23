using System;
using UnityEngine;

public class TargetShooter : MonoBehaviour
{
    public static Action OnTargetMissed;

    [SerializeField] Camera cam;

    // Optional: assign a LayerMask if you want to ignore certain layers
    [SerializeField] LayerMask raycastLayers = ~0; // Default: all layers

    void Update()
    {
        if (Timer.GameEnded)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, raycastLayers))
            {
                Target target = hit.collider.GetComponent<Target>();

                if (target != null)
                {
                    // Hit a target → count as score
                    target.Hit();
                }
                else
                {
                    // Hit something else → count as miss
                    Debug.Log("Missed! Clicked on a non-target object");
                    OnTargetMissed?.Invoke();
                }
            }
            else
            {
                // Hit nothing → count as miss
                Debug.Log("Missed! Clicked in empty space");
                OnTargetMissed?.Invoke();
            }
        }
    }
}