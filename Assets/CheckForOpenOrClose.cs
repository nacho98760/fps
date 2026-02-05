using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CheckForOpenOrClose : MonoBehaviour
{
    Vector3 doorClosedPosition;
    Quaternion doorClosedRotation;

    Vector3 doorOpenedPosition;
    Quaternion doorOpenedRotation;

    public bool isDoorClosed;

    void Start()
    {
        isDoorClosed = true;
        doorClosedPosition = transform.localPosition;
        doorClosedRotation = transform.localRotation;

        doorOpenedPosition = doorClosedPosition + new Vector3(-0.905f, 0f, -0.717f);
        doorOpenedRotation = doorClosedRotation * Quaternion.Euler(0f, -90f, 0f);
    }

    private void Update()
    {
        Vector3 targetPosition = !isDoorClosed ? doorOpenedPosition : doorClosedPosition;
        Quaternion targetRotation = !isDoorClosed ? doorOpenedRotation : doorClosedRotation;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * 10f);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * 7f);
    }
}
