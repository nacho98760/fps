using System;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public event Action<bool> OnDoorStateChanged;

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

        doorOpenedPosition = doorClosedPosition + new Vector3(0.706f, 0f, -0.74f);
        doorOpenedRotation = doorClosedRotation * Quaternion.Euler(0f, 90f, 0f);
    }

    void Update()
    {
        CheckForInteractableObjects();
        ChangeDoorPositionAndRotationBasedOnState();
    }

    void CheckForInteractableObjects()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask layer = ~LayerMask.GetMask("Ignore Raycast"); //Putting the "~" symbol at the start of the function makes the raycast hit everything except the 'IgnoreRaycast' mask

            if (Physics.Raycast(ray, out RaycastHit hit, 20f, layer))
            {
                if (hit.collider.CompareTag("Interactables"))
                {
                    if (hit.collider.GetComponent<DoorScript>())
                    {
                        DoorScript doorScript = hit.collider.GetComponent<DoorScript>();

                        doorScript.isDoorClosed = !doorScript.isDoorClosed;
                        OnDoorStateChanged?.Invoke(doorScript.isDoorClosed);
                    }
                }
            }
        }
    }

    void ChangeDoorPositionAndRotationBasedOnState()
    {
        Vector3 targetPosition = isDoorClosed ? doorClosedPosition : doorOpenedPosition;
        Quaternion targetRotation = isDoorClosed ? doorClosedRotation : doorOpenedRotation;

        transform.SetLocalPositionAndRotation(
            Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * 7f), //Position
            Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * 7f) //Rotation
        );
    }
}
