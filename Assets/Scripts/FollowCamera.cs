using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform cameraTransform;

    void LateUpdate()
    {
        Vector3 pos = cameraTransform.position;
        pos.z = transform.position.z; // keep original Z
        transform.position = pos;
    }
}