using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class UpperBodyIKDualConfig : MonoBehaviour
{
    [SerializeField] private Transform headTarget, leftHandTarget, rightHandTarget, leftElbowHint, rightElbowHint;
    [Range(0f, 1f)][SerializeField] private float headWeight = 1f, handWeight = 1f, elbowHintWeight = 1f, footWeight = 1f;
    [SerializeField] private float cameraForwardOffset = 0.2f;
    [SerializeField] private TextMeshProUGUI repCountText;
    [SerializeField] private Image repOverlay;
    [SerializeField] private float overlayDuration = 0.5f;
    [SerializeField] private ExerciseConfig frontRaiseHoldConfig; // Down -> Front
    [SerializeField] private ExerciseConfig lateralHoldConfig;    // Side -> Down

    private Animator animator;
    private Transform shoulderTransform;
    private bool isTrackingReady = false;
    private int repState = 0;
    private int repCount = 0;
    private Vector3 lastHandPos;
    private bool tooFastDuringRaise = false;
    private ExerciseConfig currentConfig;

    void Start()
    {
        animator = GetComponent<Animator>();
        shoulderTransform = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);

        if (!animator || !animator.isHuman || !frontRaiseHoldConfig || !lateralHoldConfig || !repCountText || !repOverlay || !headTarget || !rightHandTarget)
        {
            Debug.LogError("Required components missing!");
            enabled = false;
            return;
        }

        currentConfig = frontRaiseHoldConfig; // Start with Down -> Front
        StartCoroutine(WaitForTracking());
    }

    IEnumerator WaitForTracking()
    {
        while (headTarget.position.y < 0.5f) yield return null;

        Vector3 headOffset = animator.GetBoneTransform(HumanBodyBones.Head).position - transform.position;
        transform.position = headTarget.position - headOffset - Vector3.forward * cameraForwardOffset;

        isTrackingReady = true;
        repCountText.text = $"Reps: 0/{frontRaiseHoldConfig.requiredReps / 2}";
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

        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            ChangeScene();
            return;
        }

        Vector3 controllerPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        if (controllerPos == Vector3.zero) return;

        rightHandTarget.position = controllerPos;
        rightHandTarget.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

        Vector3 armDir = (rightHandTarget.position - shoulderTransform.position).normalized;
        float velocity = Vector3.Distance(rightHandTarget.position, lastHandPos) / Time.deltaTime;

        if (repState == 1 && velocity > currentConfig.maxVelocity)
        {
            tooFastDuringRaise = true;
        }

        // Switch config based on repCount phase
        currentConfig = repCount % 2 == 0 ? frontRaiseHoldConfig : lateralHoldConfig;

        float angleToStart = Vector3.Angle(armDir, currentConfig.startDirection);
        float angleToTarget = Vector3.Angle(armDir, currentConfig.targetDirection);
        float tolerance = currentConfig.angleTolerance;
        float minVel = currentConfig.minVelocity;

        int previousState = repState;
        if (repState == 0 && angleToStart < tolerance && velocity > minVel)
        {
            repState = 1;
            tooFastDuringRaise = false;
        }
        else if (repState == 1 && angleToTarget < tolerance && velocity > minVel)
        {
            repState = 2;
            if (!tooFastDuringRaise)
            {
                repCount++; // Increment on every target reached
                if (currentConfig == lateralHoldConfig) // Update UI only on Side -> Down
                {
                    int fullReps = repCount / 2;
                    repCountText.text = $"Reps: {fullReps}/{lateralHoldConfig.requiredReps / 2}";
                    StartCoroutine(ShowOverlay(Color.green));
                    if (repCount >= lateralHoldConfig.requiredReps) ChangeScene();
                }
            }
        }
        else if (repState == 2 && angleToStart < tolerance && velocity > minVel)
        {
            repState = 0;
            if (tooFastDuringRaise)
            {
                StartCoroutine(ShowOverlay(Color.red));
            }
        }

        lastHandPos = rightHandTarget.position;

        if (repState != previousState)
        {
            string log = $"RepState: {repState}, Reps: {repCount}, " +
                         $"AngleToStart: {angleToStart:F2}, AngleToTarget: {angleToTarget:F2}, " +
                         $"Velocity: {velocity:F2}, Pos: {rightHandTarget.position}, " +
                         $"Config: {(currentConfig == frontRaiseHoldConfig ? "Front" : "Lateral")}";
            Debug.Log(log);
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
        if (!string.IsNullOrEmpty(currentConfig.nextSceneName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentConfig.nextSceneName);
        }
    }
}