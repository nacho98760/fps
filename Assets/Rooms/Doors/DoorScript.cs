using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
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
    private AudioSource doorSound;

    private void Awake()
    {
        narrativeManager.OnNarrativeEventTriggered += HandleNarrativeEvent;
        doorSound = GetComponent<AudioSource>();
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
        doorSound.Play();
        OnDoorStateChanged?.Invoke(isDoorClosed);

        while (Quaternion.Angle(transform.localRotation, doorClosedRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, doorClosedRotation, Time.deltaTime * 7f);
            yield return null;
        }

        transform.localRotation = doorClosedRotation;
    }

    private void HandleNarrativeEvent(string eventName, List<string> dialogues)
    {
        if (transform.name == "Door1")
        {
            if (eventName == "Player Spawn Event")
            {
                StartCoroutine(OpenDoor());
            }
            else if (eventName == "First variant of ColorPatternTest")
            {
                StartCoroutine(CloseDoor());
            }
        }
    }
}
