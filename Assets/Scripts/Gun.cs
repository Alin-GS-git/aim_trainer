using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static Action OnTargetMissed;

    [Header("References")]
    [SerializeField] Camera cam;                  // Assign Main Camera
    [SerializeField] ParticleSystem muzzleFlash;  // Optional muzzle flash particle
    [SerializeField] AudioSource gunSound;        // AudioSource for gunshot

    [Header("Gun Settings")]
    [SerializeField] float fireRate = 0.3f;       // Time between shots
    [SerializeField] float range = 100f;          // Raycast range

    float nextTimeToFire = 0f;

    void Update()
    {
        if (Timer.GameEnded)
            return;

        // Automatic shooting while holding left mouse button
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // Play gun sound if assigned
        if (gunSound != null)
            gunSound.Play();

        // Play muzzle flash if assigned
        if (muzzleFlash != null)
            muzzleFlash.Play();

        // Shoot a ray from the center of the camera
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        // Raycast to detect objects
        bool didHit = Physics.Raycast(ray, out hit, range);

        if (didHit)
        {
            Target target = hit.collider.GetComponent<Target>();

            if (target != null)
            {
                // Hit target → count score
                target.Hit();
            }
            else
            {
                // Hit something else → count as miss
                OnTargetMissed?.Invoke();
                Debug.Log("Miss counted! Hit non-target object.");
            }
        }
        else
        {
            // Hit nothing → count as miss
            OnTargetMissed?.Invoke();
            Debug.Log("Miss counted! Hit empty space.");
        }
    }
}