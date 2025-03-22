using UnityEngine;

[CreateAssetMenu(fileName = "ExerciseConfig", menuName = "Exercise/Exercise Config", order = 1)]
public class ExerciseConfig : ScriptableObject
{
    [SerializeField, Tooltip("Normalized direction for start pose")]
    private Vector3 _startDirection = Vector3.down;
    public Vector3 startDirection => _startDirection.normalized;

    [SerializeField, Tooltip("Normalized direction for target pose")]
    private Vector3 _targetDirection = Vector3.forward;
    public Vector3 targetDirection => _targetDirection.normalized;

    [SerializeField, Range(5f, 45f), Tooltip("Max angle deviation (degrees)")]
    public float angleTolerance = 25f;

    [SerializeField, Range(0.01f, 1f), Tooltip("Min velocity for movement (m/s)")]
    public float minVelocity = 0.1f;

    [SerializeField, Range(1f, 10f), Tooltip("Max velocity before invalid (m/s)")]
    public float maxVelocity = 5f;

    [SerializeField, Range(1, 20), Tooltip("Reps to complete")]
    public int requiredReps = 5;

    [SerializeField, Tooltip("Next scene on completion (empty for none)")]
    public string nextSceneName = "";

    private void OnValidate()
    {
        if (_startDirection.sqrMagnitude < 0.01f) _startDirection = Vector3.down;
        if (_targetDirection.sqrMagnitude < 0.01f) _targetDirection = Vector3.forward;
    }
}