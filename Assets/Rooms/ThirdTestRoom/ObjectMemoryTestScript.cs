using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObjectMemoryTestScript : MonoBehaviour
{
    [SerializeField] private NarrativeManager narrativeManager;
    [SerializeField] private GameObject blackoutCanvasPanel;

    [SerializeField] private GameObject firstRoundSlots;
    [SerializeField] private GameObject secondRoundSlots;
    [SerializeField] private GameObject thirdRoundSlots;

    private GameObject slotsToShuffle;

    [NonSerialized] public bool isPlayerAllowedToPlay;
    [NonSerialized] public bool wasItTheFirstSlotObjectTouched = true;
    [NonSerialized] public bool didPlayerPick2Objects = false;

    public List<GameObject> slotObjectsOrderBeforeShuffling;

    public List<GameObject> slotsPickedToPlay;

    private bool isSwitching = false;
    [NonSerialized] public bool isSlotObjectSequenceCorrect;

    [NonSerialized] public bool isBlackoutActive;

    private void Awake()
    {
        narrativeManager.OnNarrativeEventTriggered += HandleNarrativeEvent;
    }

    private void Start()
    {
        isSlotObjectSequenceCorrect = false;
        isBlackoutActive = false;
        isPlayerAllowedToPlay = false;
        slotsPickedToPlay = new List<GameObject>();
    }

    private void HandleNarrativeEvent(string eventName, List<string> dialogues)
    {
        if (eventName == "Middle of Hallway 3")
        {
            firstRoundSlots.SetActive(true);
        }

        if (eventName == "First variant of ObjectMemoryTest")
        {
            slotsToShuffle = firstRoundSlots;
        }
        if (eventName == "Second variant of ObjectMemoryTest")
        {
            slotsToShuffle = secondRoundSlots;
            firstRoundSlots.SetActive(false);
            secondRoundSlots.SetActive(true);
        }
        if (eventName == "Third variant of ObjectMemoryTest")
        {
            slotsToShuffle = thirdRoundSlots;
            secondRoundSlots.SetActive(false);
            thirdRoundSlots.SetActive(true);
        }

        if (eventName == "Blackout")
        {
            StartCoroutine(ActivateBlackoutAndShuffle(slotsToShuffle));
            isPlayerAllowedToPlay = true;
        }

        if (eventName == "End of ObjectMemoryTest Round")
        {
            isSlotObjectSequenceCorrect = false;
            isPlayerAllowedToPlay = false;
        }

        if (eventName == "End of ObjectMemoryTest")
        {
            //---
        }
    }


    private IEnumerator ActivateBlackoutAndShuffle(GameObject roundSlotsToShuffle)
    {
        isBlackoutActive = true;
        blackoutCanvasPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        ShuffleObjects(roundSlotsToShuffle);
        yield return new WaitForSeconds(2.5f);

        blackoutCanvasPanel.SetActive(false);
        isBlackoutActive = false;
    }

    private void ShuffleObjects(GameObject roundSlotsToShuffle)
    {
        print("Shuffle started");
        List<GameObject> slotObjects = new List<GameObject>();

        slotObjectsOrderBeforeShuffling.Clear();

        foreach (Transform slot in roundSlotsToShuffle.transform)
        {
            slotObjectsOrderBeforeShuffling.Add(slot.GetChild(0).gameObject);
            slotObjects.Add(slot.GetChild(0).gameObject);
        }

        do
        {
            // Fisher-Yates shuffle
            for (int i = slotObjects.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                (slotObjects[i], slotObjects[j]) = (slotObjects[j], slotObjects[i]);
            }
        } while (slotObjects == slotObjectsOrderBeforeShuffling);

        for (int i = 0; i < roundSlotsToShuffle.transform.childCount; i++)
        {
            slotObjects[i].transform.SetParent(roundSlotsToShuffle.transform.GetChild(i).transform, false);
        }
    }


    public void SwitchObjects()
    {
        if (isSwitching)
            return;
        
        isSwitching = true;

        GameObject firstObject = slotsPickedToPlay[0].transform.GetChild(0).gameObject;
        GameObject secondObject = slotsPickedToPlay[1].transform.GetChild(0).gameObject;

        SlotObjectModelScript firstSlotObjectModel = firstObject.transform.GetChild(0).gameObject.GetComponent<SlotObjectModelScript>();
        SlotObjectModelScript secondSlotObjectModel = secondObject.transform.GetChild(0).gameObject.GetComponent<SlotObjectModelScript>();

        //-------Object Switch-------
        firstObject.transform.parent = slotsPickedToPlay[1].transform;
        secondObject.transform.parent = slotsPickedToPlay[0].transform;

        StartCoroutine(ObjectSwitchTransition(firstObject, slotsPickedToPlay[1]));
        StartCoroutine(ObjectSwitchTransition(secondObject, slotsPickedToPlay[0]));
        //-------Object Switch-------

        firstSlotObjectModel.objectState = ObjectState.NotHoveredNorSelected;
        secondSlotObjectModel.objectState = ObjectState.NotHoveredNorSelected;

        slotsPickedToPlay.Clear();

        //Reset values
        wasItTheFirstSlotObjectTouched = true;
        isSwitching = false;

        isSlotObjectSequenceCorrect = CheckIfItsTheCorrectOrder();
    }


    private IEnumerator ObjectSwitchTransition(GameObject obj, GameObject slot)
    {
        float timePassed = 0f;
        float transitionDuration = 0.75f;

        while (timePassed < transitionDuration)
        {
            timePassed += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(obj.transform.position, slot.transform.position, timePassed / transitionDuration);
            yield return null;
        }

        obj.transform.position = slot.transform.position;
    }


    private bool CheckIfItsTheCorrectOrder()
    {
        bool isSequenceCorrect = true;

        for (int i = 0; i < slotsToShuffle.transform.childCount; i++)
        {
            Transform slot = slotsToShuffle.transform.GetChild(i);

            if (slot.transform.GetChild(0).gameObject != slotObjectsOrderBeforeShuffling[i])
            {
                isSequenceCorrect = false;
                break;
            }
        }

        return isSequenceCorrect;
    }
}
