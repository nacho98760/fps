using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorScript : MonoBehaviour
{
    public event Action<bool> OnDoorStateChanged;

    Quaternion doorClosedRotation;
    Quaternion doorOpenedRotation;

    public bool isDoorClosed;

    void Start()
    {
        isDoorClosed = true;
        doorClosedRotation = transform.localRotation;
        doorOpenedRotation = doorClosedRotation * Quaternion.Euler(0f, -90f, 0f);
    }

    public IEnumerator OpenDoor()
    {
        isDoorClosed = false;
        OnDoorStateChanged?.Invoke(isDoorClosed);

        while (Quaternion.Angle(transform.localRotation, doorOpenedRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation,doorOpenedRotation,Time.deltaTime * 7f);
            yield return null; 
        }

        transform.localRotation = doorOpenedRotation; 
    }

    public IEnumerator CloseDoor()
    {
        isDoorClosed = true;
        OnDoorStateChanged?.Invoke(isDoorClosed);

        while (Quaternion.Angle(transform.localRotation, doorClosedRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, doorClosedRotation, Time.deltaTime * 7f);
            yield return null;
        }

        transform.localRotation = doorClosedRotation;
    }
}
