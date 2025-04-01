// // using UnityEngine;
// // using System.Collections;

// // [RequireComponent(typeof(Animator))]
// // public class UpperBodyIK : MonoBehaviour
// // {
// //     public Transform headTarget;      // CenterEyeAnchor
// //     public Transform leftHandTarget;  // LeftControllerAnchor
// //     public Transform rightHandTarget; // RightControllerAnchor
// //     private Animator animator;
// //     public Transform leftElbowHint;
// //     public Transform rightElbowHint;

// //     [Range(0f, 1f)] public float headWeight = 1f;
// //     [Range(0f, 1f)] public float handWeight = 1f;
// //     [Range(0f, 1f)] public float elbowHintWeight = 1f;
// //     [Range(0f, 1f)] public float footWeight = 1f;

// //     [SerializeField] private float cameraForwardOffset = 0.2f; // Adjust this (0.2–0.5m) for camera distance

// //     private bool isTrackingReady = false;

// //     void Start()
// //     {
// //         animator = GetComponent<Animator>();
// //         if (!animator) Debug.LogError("Animator missing!");
// //         if (!headTarget) Debug.LogError("Head target missing!");

// //         StartCoroutine(WaitForTracking());
// //     }

// //     IEnumerator WaitForTracking()
// //     {
// //         while (headTarget.position.y < 0.5f)
// //         {
// //             Debug.Log($"Waiting for tracking - Headset Y: {headTarget.position.y}");
// //             yield return null;
// //         }

// //         Vector3 headOffset = animator.GetBoneTransform(HumanBodyBones.Head).position - transform.position;
// //         transform.position = headTarget.position - headOffset - Vector3.forward * cameraForwardOffset; // Shift avatar back
// //         isTrackingReady = true;

// //         Debug.Log($"Tracking Ready - Root: {transform.position}, Avatar Head: {animator.GetBoneTransform(HumanBodyBones.Head).position}, Headset: {headTarget.position}");
// //     }

// //     void LateUpdate()
// //     {
// //         if (headTarget != null && isTrackingReady)
// //         {
// //             Vector3 headOffset = animator.GetBoneTransform(HumanBodyBones.Head).position - transform.position;
// //             Vector3 targetRootPos = headTarget.position - headOffset - Vector3.forward * cameraForwardOffset; // Keep avatar back
// //             transform.position = Vector3.Lerp(transform.position, targetRootPos, Time.deltaTime * 10f);
// //             Debug.Log($"Headset: {headTarget.position}, Avatar Head: {animator.GetBoneTransform(HumanBodyBones.Head).position}, Root: {transform.position}");
// //         }
// //     }

// //     void OnAnimatorIK(int layerIndex)
// //     {
// //         if (!animator) return;

// //         if (headTarget != null)
// //         {
// //             animator.SetLookAtPosition(headTarget.position);
// //             animator.SetLookAtWeight(headWeight);
// //         }

// //         if (leftHandTarget != null)
// //         {
// //             animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
// //             animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
// //             animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, handWeight);
// //             animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, handWeight);

// //             if (leftElbowHint != null)
// //             {
// //                 animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowHint.position);
// //                 animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, elbowHintWeight);
// //             }
// //         }

// //         if (rightHandTarget != null)
// //         {
// //             animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
// //             animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
// //             animator.SetIKPositionWeight(AvatarIKGoal.RightHand, handWeight);
// //             animator.SetIKRotationWeight(AvatarIKGoal.RightHand, handWeight);

// //             if (rightElbowHint != null)
// //             {
// //                 animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowHint.position);
// //                 animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, elbowHintWeight);
// //             }
// //         }

// //         Vector3 rootXZ = new Vector3(transform.position.x, 0f, transform.position.z);
// //         animator.SetIKPosition(AvatarIKGoal.LeftFoot, rootXZ + new Vector3(-0.2f, 0f, 0f));
// //         animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.identity);
// //         animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, footWeight);
// //         animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, footWeight);

// //         animator.SetIKPosition(AvatarIKGoal.RightFoot, rootXZ + new Vector3(0.2f, 0f, 0f));
// //         animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.identity);
// //         animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, footWeight);
// //         animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, footWeight);
// //     }

// //     void Update()
// //     {
// //         if (leftElbowHint != null && rightElbowHint != null)
// //         {
// //             Vector3 leftShoulder = animator.GetBoneTransform(HumanBodyBones.LeftShoulder).position;
// //             Vector3 rightShoulder = animator.GetBoneTransform(HumanBodyBones.RightShoulder).position;
// //             Vector3 torsoCenter = (leftShoulder + rightShoulder) * 0.5f;

// //             Vector3 leftHandPos = leftHandTarget.position;
// //             float leftDistanceToTorso = Vector3.Distance(leftHandPos, torsoCenter);
// //             Vector3 leftArmDir = (leftHandPos - leftShoulder).normalized;
// //             if (leftDistanceToTorso < 0.5f)
// //             {
// //                 leftElbowHint.position = leftShoulder - Vector3.right * 0.3f - Vector3.up * 0.2f;
// //             }
// //             else
// //             {
// //                 leftElbowHint.position = leftShoulder + leftArmDir * 0.3f - Vector3.up * 0.2f;
// //             }

// //             Vector3 rightHandPos = rightHandTarget.position;
// //             float rightDistanceToTorso = Vector3.Distance(rightHandPos, torsoCenter);
// //             Vector3 rightArmDir = (rightHandPos - rightShoulder).normalized;
// //             if (rightDistanceToTorso < 0.5f)
// //             {
// //                 rightElbowHint.position = rightShoulder + Vector3.right * 0.3f - Vector3.up * 0.2f;
// //             }
// //             else
// //             {
// //                 rightElbowHint.position = rightShoulder + rightArmDir * 0.3f - Vector3.up * 0.2f;
// //             }
// //         }
// //     }
// // }

// using UnityEngine.UI;
// using UnityEngine;
// using TMPro;
// using System.Collections;

// [RequireComponent(typeof(Animator))]
// public class UpperBodyIK : MonoBehaviour
// {
//     // IK Fields
//     public Transform headTarget, leftHandTarget, rightHandTarget, leftElbowHint, rightElbowHint;
//     private Animator animator;
//     [Range(0f, 1f)] public float headWeight = 1f, handWeight = 1f, elbowHintWeight = 1f, footWeight = 1f;
//     [SerializeField] private float cameraForwardOffset = 0.2f;
//     private bool isTrackingReady = false;

//     // Activity Monitoring Fields
//     public TextMeshProUGUI repCountText;
//     public Image repOverlay;
//     public float overlayDuration = 0.5f;
//     public string nextSceneName;

//     // Monitoring Settings
//     public float angleTolerance = 25f; // Slightly stricter (was 30°)
//     private float minVelocity = 0.1f; // Min to register movement
//     private float maxVelocity = 5f; // Max before rep is "too fast"
//     private int repState = 0; // 0: Waiting for start, 1: Waiting for target, 2: Waiting for start again
//     private int repCount = 0;
//     private Vector3 lastHandPos;
//     private bool tooFastDuringRaise = false;

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         if (!animator || !animator.isHuman)
//         {
//             Debug.LogError("Animator setup invalid!");
//             return;
//         }

//         if (!headTarget || !repCountText || !repOverlay)
//         {
//             Debug.LogError("Required components missing!");
//             return;
//         }

//         StartCoroutine(WaitForTracking());
//     }

//     IEnumerator WaitForTracking()
//     {
//         while (headTarget.position.y < 0.5f) yield return null;

//         Vector3 headOffset = animator.GetBoneTransform(HumanBodyBones.Head).position - transform.position;
//         transform.position = headTarget.position - headOffset - Vector3.forward * cameraForwardOffset;

//         isTrackingReady = true;
//         repCountText.text = "Reps: 0/5";
//         lastHandPos = rightHandTarget.position; // Use controller directly
//     }

//     void LateUpdate()
//     {
//         if (headTarget != null && isTrackingReady)
//         {
//             Vector3 headOffset = animator.GetBoneTransform(HumanBodyBones.Head).position - transform.position;
//             Vector3 targetRootPos = headTarget.position - headOffset - Vector3.forward * cameraForwardOffset;
//             transform.position = Vector3.Lerp(transform.position, targetRootPos, Time.deltaTime * 10f);
//         }
//     }

//     void OnAnimatorIK(int layerIndex)
//     {
//         if (!animator || !isTrackingReady) return;

//         animator.SetLookAtPosition(headTarget.position);
//         animator.SetLookAtWeight(headWeight);

//         animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
//         animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
//         animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, handWeight);
//         animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, handWeight);
//         if (leftElbowHint) animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowHint.position);
//         animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, elbowHintWeight);

//         animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
//         animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
//         animator.SetIKPositionWeight(AvatarIKGoal.RightHand, handWeight);
//         animator.SetIKRotationWeight(AvatarIKGoal.RightHand, handWeight);
//         if (rightElbowHint) animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowHint.position);
//         animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, elbowHintWeight);

//         Vector3 rootXZ = new Vector3(transform.position.x, 0f, transform.position.z);
//         animator.SetIKPosition(AvatarIKGoal.LeftFoot, rootXZ + new Vector3(-0.2f, 0f, 0f));
//         animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.identity);
//         animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, footWeight);
//         animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, footWeight);

//         animator.SetIKPosition(AvatarIKGoal.RightFoot, rootXZ + new Vector3(0.2f, 0f, 0f));
//         animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.identity);
//         animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, footWeight);
//         animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, footWeight);
//     }

//     void Update()
//     {
//         if (!isTrackingReady) return;

//         // Update controller positions
//         Vector3 controllerPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
//         if (controllerPos == Vector3.zero)
//         {
//             Debug.LogWarning("Right controller not tracking!");
//             return;
//         }
//         rightHandTarget.position = controllerPos;
//         rightHandTarget.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

//         // Calculate arm direction and velocity
//         Transform userShoulder = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
//         Vector3 userArmDir = (rightHandTarget.position - userShoulder.position).normalized;
//         float velocity = Vector3.Distance(rightHandTarget.position, lastHandPos) / Time.deltaTime;

//         // Check velocity during raising
//         if (repState == 1 && velocity > maxVelocity)
//         {
//             tooFastDuringRaise = true;
//         }

//         // Check angles to predefined poses
//         float angleToStart = Vector3.Angle(userArmDir, Vector3.down); // Arms down
//         float angleToTarget = Vector3.Angle(userArmDir, Vector3.forward); // Arms up (front raise)

//         // State machine
//         int previousState = repState;
//         if (repState == 0 && angleToStart < angleTolerance && velocity > minVelocity)
//         {
//             repState = 1;
//             tooFastDuringRaise = false; // Reset flag
//         }
//         else if (repState == 1 && angleToTarget < angleTolerance && velocity > minVelocity)
//         {
//             repState = 2;
//         }
//         else if (repState == 2 && angleToStart < angleTolerance && velocity > minVelocity)
//         {
//             repState = 0;
//             if (!tooFastDuringRaise)
//             {
//                 repCount++;
//                 repCountText.text = "Reps: " + repCount + "/5";
//                 StartCoroutine(ShowOverlay(Color.green));
//                 if (repCount >= 5)
//                 {
//                     ChangeScene();
//                 }
//             }
//             else
//             {
//                 StartCoroutine(ShowOverlay(Color.red));
//             }
//         }

//         // Debug only on state change
//         if (repState != previousState)
//         {
//             string stateMessage = repState switch
//             {
//                 0 => tooFastDuringRaise ? "Rep too fast, reset" : "Matched start pose (or rep completed: " + repCount + ")",
//                 1 => "Matched start pose, waiting for target",
//                 2 => "Matched target pose, waiting for start",
//                 _ => ""
//             };
//             Debug.Log($"RepState changed to: {repState}, {stateMessage}, " +
//                       $"AngleToStart: {angleToStart:F2}, AngleToTarget: {angleToTarget:F2}, " +
//                       $"Velocity: {velocity:F2}, RightHandTarget: {rightHandTarget.position}");
//         }

//         lastHandPos = rightHandTarget.position;
//     }

//     IEnumerator ShowOverlay(Color color)
//     {
//         repOverlay.color = new Color(color.r, color.g, color.b, 0.5f);
//         yield return new WaitForSeconds(overlayDuration);
//         repOverlay.color = new Color(0, 0, 0, 0);
//     }

//     void ChangeScene()
//     {
//         if (!string.IsNullOrEmpty(nextSceneName))
//         {
//             UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
//         }
//         else
//         {
//             Debug.LogWarning("No next scene specified!");
//         }
//     }
// }

using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class UpperBodyIK : MonoBehaviour
{
    [SerializeField] private Transform headTarget, leftHandTarget, rightHandTarget, leftElbowHint, rightElbowHint;
    [Range(0f, 1f)][SerializeField] private float headWeight = 1f, handWeight = 1f, elbowHintWeight = 1f, footWeight = 1f;
    [SerializeField] private float cameraForwardOffset = 0.2f;
    [SerializeField] private TextMeshProUGUI repCountText;
    [SerializeField] private Image repOverlay;
    [SerializeField] private float overlayDuration = 0.5f;
    [SerializeField] private ExerciseConfig exerciseConfig;

    private Animator animator;
    private Transform shoulderTransform;
    private bool isTrackingReady = false;
    private int repState = 0;
    private int repCount = 0;
    private Vector3 lastHandPos;
    private bool tooFastDuringRaise = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        shoulderTransform = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);

        if (!animator || !animator.isHuman || !exerciseConfig || !repCountText || !repOverlay || !headTarget || !rightHandTarget)
        {
            Debug.LogError("Required components missing!");
            enabled = false;
            return;
        }

        StartCoroutine(WaitForTracking());
    }

    IEnumerator WaitForTracking()
    {
        while (headTarget.position.y < 0.5f) yield return null;

        Vector3 headOffset = animator.GetBoneTransform(HumanBodyBones.Head).position - transform.position;
        transform.position = headTarget.position - headOffset - Vector3.forward * cameraForwardOffset;

        isTrackingReady = true;
        repCountText.text = $"Reps: 0/{exerciseConfig.requiredReps}";
        lastHandPos = rightHandTarget.position;
    }

    void LateUpdate()
    {
        if (!isTrackingReady) return;

        Vector3 headOffset = animator.GetBoneTransform(HumanBodyBones.Head).position - transform.position;
        transform.position = Vector3.Lerp(transform.position, headTarget.position - headOffset - Vector3.forward * cameraForwardOffset, Time.deltaTime * 10f);
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (!isTrackingReady) return;

        animator.SetLookAtPosition(headTarget.position);
        animator.SetLookAtWeight(headWeight);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, handWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, handWeight);
        if (leftElbowHint) animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowHint.position);
        animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, elbowHintWeight);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, handWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, handWeight);
        if (rightElbowHint) animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowHint.position);
        animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, elbowHintWeight);

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
        if (!isTrackingReady) return;

        Vector3 controllerPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        if (controllerPos == Vector3.zero) return;

        rightHandTarget.position = controllerPos;
        rightHandTarget.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

        Vector3 armDir = (rightHandTarget.position - shoulderTransform.position).normalized;
        float velocity = Vector3.Distance(rightHandTarget.position, lastHandPos) / Time.deltaTime;

        if (repState == 1 && velocity > exerciseConfig.maxVelocity)
        {
            tooFastDuringRaise = true;
        }

        float angleToStart = Vector3.Angle(armDir, exerciseConfig.startDirection);
        float angleToTarget = Vector3.Angle(armDir, exerciseConfig.targetDirection);
        float tolerance = exerciseConfig.angleTolerance;
        float minVel = exerciseConfig.minVelocity;

        int previousState = repState;
        if (repState == 0 && angleToStart < tolerance && velocity > minVel)
        {
            repState = 1;
            tooFastDuringRaise = false;
        }
        else if (repState == 1 && angleToTarget < tolerance && velocity > minVel)
        {
            repState = 2;
        }
        else if (repState == 2 && angleToStart < tolerance && velocity > minVel)
        {
            repState = 0;
            if (!tooFastDuringRaise)
            {
                repCount++;
                repCountText.text = $"Reps: {repCount}/{exerciseConfig.requiredReps}";
                StartCoroutine(ShowOverlay(Color.green));
                if (repCount >= exerciseConfig.requiredReps) ChangeScene();
            }
            else
            {
                StartCoroutine(ShowOverlay(Color.red));
            }
        }

        lastHandPos = rightHandTarget.position;

        if (repState != previousState)
        {
            string log = $"RepState: {repState}, Reps: {repCount}, " +
                         $"AngleToStart: {angleToStart:F2}, AngleToTarget: {angleToTarget:F2}, " +
                         $"Velocity: {velocity:F2}, Pos: {rightHandTarget.position}";
            Debug.Log(log); // Still logs to adb logcat
        }
    }

    private IEnumerator ShowOverlay(Color color)
    {
        repOverlay.color = new Color(color.r, color.g, color.b, 0.5f);
        yield return new WaitForSeconds(overlayDuration);
        repOverlay.color = Color.clear;
    }

    private void ChangeScene()
    {
        if (!string.IsNullOrEmpty(exerciseConfig.nextSceneName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(exerciseConfig.nextSceneName);
        }
    }
}