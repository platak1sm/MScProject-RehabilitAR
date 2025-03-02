using UnityEngine;

public class MirrorCamera : MonoBehaviour
{
    public Transform headTarget;      // CenterEyeAnchor from OVRCameraRig
    public Transform mirrorPlane;     // MirrorPlane Quad
    private Camera mirrorCam;

    void Start()
    {
        mirrorCam = GetComponent<Camera>();
        if (!headTarget) Debug.LogError("Head target missing!");
        if (!mirrorPlane) Debug.LogError("Mirror plane missing!");
    }

    void LateUpdate()
    {
        if (headTarget != null && mirrorPlane != null)
        {
            // Mirror plane’s normal (assuming Quad faces -Z, normal is +Z)
            Vector3 mirrorNormal = mirrorPlane.forward;

            // Reflect headset position across the mirror plane
            Vector3 headToMirror = mirrorPlane.position - headTarget.position;
            float distanceToMirror = Vector3.Dot(headToMirror, mirrorNormal);
            Vector3 reflectedPosition = headTarget.position + 2 * distanceToMirror * mirrorNormal;

            // Set mirror camera position
            transform.position = reflectedPosition;

            // Reflect headset rotation
            Vector3 headForward = headTarget.forward;
            Vector3 headUp = headTarget.up;
            Vector3 reflectedForward = Vector3.Reflect(headForward, mirrorNormal);
            transform.rotation = Quaternion.LookRotation(reflectedForward, headUp);

            Debug.Log($"Headset: {headTarget.position}, MirrorCam: {transform.position}");
        }
    }
}