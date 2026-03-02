using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorScript : MonoBehaviour
{
    public event Action<bool> OnDoorStateChanged;

    Quaternion doorClosedRotation;
    Quaternion doorOpenedRotation;

    [SerializeField] private NarrativeManager narrativeManager;

    public bool isDoorClosed;

    private void Awake()
    {
        narrativeManager.OnNarrativeEventTriggered += HandleNarrativeEvent;
    }

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

    private void HandleNarrativeEvent(string eventName)
    {
        if (eventName == "Player Spawn Event")
        {
            if (transform.name == "Door1")
            {
                StartCoroutine(OpenDoor());
            }
        }
        else if (eventName == "Player Enters First Test Room")
        { 
            if (transform.name == "Door1")
            {
                StartCoroutine(CloseDoor());
            }
        }
    }
}
