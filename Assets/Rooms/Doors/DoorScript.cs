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
        switch (eventName)
        {
            case "Player Spawn Event":

                if (transform.name == "Door1" && isDoorClosed)
                {
                    StartCoroutine(OpenDoor());
                }
                break;


            case "Middle of Hallway 1":

                if (transform.name == "Door1" && isDoorClosed == false)
                {
                    StartCoroutine(CloseDoor());
                }
                if (transform.name == "HallwayDoor1" && isDoorClosed)
                {
                    StartCoroutine(OpenDoor());
                }
                break;


            case "First variant of ColorPatternTest":

                if (transform.name == "HallwayDoor1" && isDoorClosed == false)
                {
                    StartCoroutine(CloseDoor());
                }
                break;


            case "End of ColorPatternTest":
                if (transform.name == "Door2" && isDoorClosed)
                {
                    StartCoroutine(OpenDoor());
                }
                break;


            case "Middle of Hallway 2":

                if (transform.name == "Door2" && isDoorClosed == false)
                {
                    StartCoroutine(CloseDoor());
                }
                if (transform.name == "HallwayDoor2" && isDoorClosed)
                {
                    StartCoroutine(OpenDoor());
                }
                break;


            case "Start of ImageAssociationTest":

                if (transform.name == "HallwayDoor2" && isDoorClosed == false)
                {
                    StartCoroutine(CloseDoor());
                }
                break;


            case "End of ImageAssociationTest":

                if (transform.name == "Door3" && isDoorClosed)
                {
                    StartCoroutine(OpenDoor());
                }
                break;


            case "Middle of Hallway 3":

                if (transform.name == "Door3" && isDoorClosed == false)
                {
                    StartCoroutine(CloseDoor());
                }
                if (transform.name == "HallwayDoor3" && isDoorClosed)
                {
                    StartCoroutine(OpenDoor());
                }
                break;


            case "First variant of ObjectMemoryTest":

                if (transform.name == "HallwayDoor3" && isDoorClosed == false)
                {
                    StartCoroutine(CloseDoor());
                }
                break;


            case "End of ObjectMemoryTest":

                if (transform.name == "Door4" && isDoorClosed)
                {
                    StartCoroutine(OpenDoor());
                }
                break;


            case "Middle of Hallway 4":

                if (transform.name == "Door4" && isDoorClosed == false)
                {
                    StartCoroutine(CloseDoor());
                }
                if (transform.name == "HallwayDoor4" && isDoorClosed)
                {
                    print("Hey");
                    StartCoroutine(OpenDoor());
                }
                break;
        }
    }
}
