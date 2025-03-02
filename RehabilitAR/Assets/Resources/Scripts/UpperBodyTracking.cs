// using UnityEngine;
// using System.Collections;

// [RequireComponent(typeof(Animator))]
// public class UpperBodyIK : MonoBehaviour
// {
//     public Transform headTarget;      // CenterEyeAnchor
//     public Transform leftHandTarget;  // LeftControllerAnchor
//     public Transform rightHandTarget; // RightControllerAnchor
//     private Animator animator;
//     public Transform leftElbowHint;
//     public Transform rightElbowHint;

//     [Range(0f, 1f)] public float headWeight = 1f;
//     [Range(0f, 1f)] public float handWeight = 1f;
//     [Range(0f, 1f)] public float elbowHintWeight = 1f;
//     [Range(0f, 1f)] public float footWeight = 1f; // Max weight to force straight legs

//     private bool isTrackingReady = false;

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         if (!animator) Debug.LogError("Animator missing!");
//         if (!headTarget) Debug.LogError("Head target missing!");

//         StartCoroutine(WaitForTracking());
//     }

//     IEnumerator WaitForTracking()
//     {
//         while (headTarget.position.y < 0.5f)
//         {
//             Debug.Log($"Waiting for tracking - Headset Y: {headTarget.position.y}");
//             yield return null;
//         }

//         Vector3 headOffset = animator.GetBoneTransform(HumanBodyBones.Head).position - transform.position;
//         transform.position = headTarget.position - headOffset;
//         isTrackingReady = true;

//         Debug.Log($"Tracking Ready - Root: {transform.position}, Avatar Head: {animator.GetBoneTransform(HumanBodyBones.Head).position}, Headset: {headTarget.position}");
//     }

//     void LateUpdate()
//     {
//         if (headTarget != null && isTrackingReady)
//         {
//             Vector3 headOffset = animator.GetBoneTransform(HumanBodyBones.Head).position - transform.position;
//             transform.position = Vector3.Lerp(transform.position, headTarget.position - headOffset, Time.deltaTime * 10f);
//             Debug.Log($"Headset: {headTarget.position}, Avatar Head: {animator.GetBoneTransform(HumanBodyBones.Head).position}, Root: {transform.position}");
//         }
//     }

//     void OnAnimatorIK(int layerIndex)
//     {
//         if (!animator) return;

//         if (headTarget != null)
//         {
//             animator.SetLookAtPosition(headTarget.position);
//             animator.SetLookAtWeight(headWeight);
//         }

//         if (leftHandTarget != null)
//         {
//             animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
//             animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
//             animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, handWeight);
//             animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, handWeight);

//             if (leftElbowHint != null)
//             {
//                 animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowHint.position);
//                 animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, elbowHintWeight);
//             }
//         }

//         if (rightHandTarget != null)
//         {
//             animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
//             animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
//             animator.SetIKPositionWeight(AvatarIKGoal.RightHand, handWeight);
//             animator.SetIKRotationWeight(AvatarIKGoal.RightHand, handWeight);

//             if (rightElbowHint != null)
//             {
//                 animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowHint.position);
//                 animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, elbowHintWeight);
//             }
//         }

//         // Ground feet at absolute floor level (Y=0)
//         Vector3 rootXZ = new Vector3(transform.position.x, 0f, transform.position.z); // Floor-level root
//         animator.SetIKPosition(AvatarIKGoal.LeftFoot, rootXZ + new Vector3(-0.2f, 0f, 0f)); // Left foot at Y=0
//         animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.identity);
//         animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, footWeight);
//         animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, footWeight);

//         animator.SetIKPosition(AvatarIKGoal.RightFoot, rootXZ + new Vector3(0.2f, 0f, 0f)); // Right foot at Y=0
//         animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.identity);
//         animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, footWeight);
//         animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, footWeight);
//     }

//     void Update()
//     {
//         if (leftElbowHint != null && rightElbowHint != null)
//         {
//             Vector3 leftShoulder = animator.GetBoneTransform(HumanBodyBones.LeftShoulder).position;
//             Vector3 rightShoulder = animator.GetBoneTransform(HumanBodyBones.RightShoulder).position;
//             Vector3 torsoCenter = (leftShoulder + rightShoulder) * 0.5f;

//             Vector3 leftHandPos = leftHandTarget.position;
//             float leftDistanceToTorso = Vector3.Distance(leftHandPos, torsoCenter);
//             Vector3 leftArmDir = (leftHandPos - leftShoulder).normalized;
//             if (leftDistanceToTorso < 0.5f)
//             {
//                 leftElbowHint.position = leftShoulder - Vector3.right * 0.3f - Vector3.up * 0.2f;
//             }
//             else
//             {
//                 leftElbowHint.position = leftShoulder + leftArmDir * 0.3f - Vector3.up * 0.2f;
//             }

//             Vector3 rightHandPos = rightHandTarget.position;
//             float rightDistanceToTorso = Vector3.Distance(rightHandPos, torsoCenter);
//             Vector3 rightArmDir = (rightHandPos - rightShoulder).normalized;
//             if (rightDistanceToTorso < 0.5f)
//             {
//                 rightElbowHint.position = rightShoulder + Vector3.right * 0.3f - Vector3.up * 0.2f;
//             }
//             else
//             {
//                 rightElbowHint.position = rightShoulder + rightArmDir * 0.3f - Vector3.up * 0.2f;
//             }
//         }
//     }
// }

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class UpperBodyIK : MonoBehaviour
{
    public Transform headTarget;      // CenterEyeAnchor
    public Transform leftHandTarget;  // LeftControllerAnchor
    public Transform rightHandTarget; // RightControllerAnchor
    private Animator animator;
    public Transform leftElbowHint;
    public Transform rightElbowHint;

    [Range(0f, 1f)] public float headWeight = 1f;
    [Range(0f, 1f)] public float handWeight = 1f;
    [Range(0f, 1f)] public float elbowHintWeight = 1f;
    [Range(0f, 1f)] public float footWeight = 1f;

    [SerializeField] private float cameraForwardOffset = 0.2f; // Adjust this (0.2–0.5m) for camera distance

    private bool isTrackingReady = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (!animator) Debug.LogError("Animator missing!");
        if (!headTarget) Debug.LogError("Head target missing!");

        StartCoroutine(WaitForTracking());
    }

    IEnumerator WaitForTracking()
    {
        while (headTarget.position.y < 0.5f)
        {
            Debug.Log($"Waiting for tracking - Headset Y: {headTarget.position.y}");
            yield return null;
        }

        Vector3 headOffset = animator.GetBoneTransform(HumanBodyBones.Head).position - transform.position;
        transform.position = headTarget.position - headOffset - Vector3.forward * cameraForwardOffset; // Shift avatar back
        isTrackingReady = true;

        Debug.Log($"Tracking Ready - Root: {transform.position}, Avatar Head: {animator.GetBoneTransform(HumanBodyBones.Head).position}, Headset: {headTarget.position}");
    }

    void LateUpdate()
    {
        if (headTarget != null && isTrackingReady)
        {
            Vector3 headOffset = animator.GetBoneTransform(HumanBodyBones.Head).position - transform.position;
            Vector3 targetRootPos = headTarget.position - headOffset - Vector3.forward * cameraForwardOffset; // Keep avatar back
            transform.position = Vector3.Lerp(transform.position, targetRootPos, Time.deltaTime * 10f);
            Debug.Log($"Headset: {headTarget.position}, Avatar Head: {animator.GetBoneTransform(HumanBodyBones.Head).position}, Root: {transform.position}");
        }
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (!animator) return;

        if (headTarget != null)
        {
            animator.SetLookAtPosition(headTarget.position);
            animator.SetLookAtWeight(headWeight);
        }

        if (leftHandTarget != null)
        {
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, handWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, handWeight);

            if (leftElbowHint != null)
            {
                animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowHint.position);
                animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, elbowHintWeight);
            }
        }

        if (rightHandTarget != null)
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, handWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, handWeight);

            if (rightElbowHint != null)
            {
                animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowHint.position);
                animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, elbowHintWeight);
            }
        }

        Vector3 rootXZ = new Vector3(transform.position.x, 0f, transform.position.z);
        animator.SetIKPosition(AvatarIKGoal.LeftFoot, rootXZ + new Vector3(-0.2f, 0f, 0f));
        animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.identity);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, footWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, footWeight);

        animator.SetIKPosition(AvatarIKGoal.RightFoot, rootXZ + new Vector3(0.2f, 0f, 0f));
        animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.identity);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, footWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, footWeight);
    }

    void Update()
    {
        if (leftElbowHint != null && rightElbowHint != null)
        {
            Vector3 leftShoulder = animator.GetBoneTransform(HumanBodyBones.LeftShoulder).position;
            Vector3 rightShoulder = animator.GetBoneTransform(HumanBodyBones.RightShoulder).position;
            Vector3 torsoCenter = (leftShoulder + rightShoulder) * 0.5f;

            Vector3 leftHandPos = leftHandTarget.position;
            float leftDistanceToTorso = Vector3.Distance(leftHandPos, torsoCenter);
            Vector3 leftArmDir = (leftHandPos - leftShoulder).normalized;
            if (leftDistanceToTorso < 0.5f)
            {
                leftElbowHint.position = leftShoulder - Vector3.right * 0.3f - Vector3.up * 0.2f;
            }
            else
            {
                leftElbowHint.position = leftShoulder + leftArmDir * 0.3f - Vector3.up * 0.2f;
            }

            Vector3 rightHandPos = rightHandTarget.position;
            float rightDistanceToTorso = Vector3.Distance(rightHandPos, torsoCenter);
            Vector3 rightArmDir = (rightHandPos - rightShoulder).normalized;
            if (rightDistanceToTorso < 0.5f)
            {
                rightElbowHint.position = rightShoulder + Vector3.right * 0.3f - Vector3.up * 0.2f;
            }
            else
            {
                rightElbowHint.position = rightShoulder + rightArmDir * 0.3f - Vector3.up * 0.2f;
            }
        }
    }
}