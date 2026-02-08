using UnityEngine;

public class RotateDecayEffect : MonoBehaviour
{
    public Vector3 rotationAxis;
    public float maxAngle;
    public float stabilityThresholdToActivateBelow;

    ObservableObject observable;
    Quaternion baseRotation;

    void Start()
    {
        observable = GetComponent<ObservableObject>();
        baseRotation = transform.localRotation;
    }

    void Update()
    {
        float stability = observable.Stability;

        if (stability > stabilityThresholdToActivateBelow)
            return;

        float t = Mathf.InverseLerp(stabilityThresholdToActivateBelow, 0f, stability);
        float angle = Mathf.Lerp(0f, maxAngle, t);

        transform.localRotation = baseRotation * Quaternion.AngleAxis(angle, rotationAxis);
    }
}
