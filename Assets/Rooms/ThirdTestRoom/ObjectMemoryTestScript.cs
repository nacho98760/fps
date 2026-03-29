using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMemoryTestScript : MonoBehaviour
{
    [SerializeField] private NarrativeManager narrativeManager;
    [SerializeField] private GameObject blackoutCanvasPanel;
    [SerializeField] private GameObject[] slots;

    [NonSerialized] public bool isPlayerAllowedToPlay;
    [NonSerialized] public bool wasItTheFirstSlotObjectTouched = true;
    [NonSerialized] public bool didPlayerPick2Objects = false;

    public List<GameObject> slotObjectsOrderBeforeShuffling;

    public GameObject[] slotsPickedToPlay;

    private bool isSwitching = false;
    public bool isSlotObjectSequenceCorrect;

    private void Awake()
    {
        narrativeManager.OnNarrativeEventTriggered += HandleNarrativeEvent;
    }

    private void Start()
    {
        isSlotObjectSequenceCorrect = false;
        isPlayerAllowedToPlay = false;
        slotsPickedToPlay = new GameObject[2];
        ShuffleObjects();
    }

    private void HandleNarrativeEvent(string eventName, List<string> dialogues)
    {
        if (eventName == "Blackout")
        {
            StartCoroutine(ActivateBlackoutAndShuffle());
            isPlayerAllowedToPlay = true;
        }
    }

    private IEnumerator ActivateBlackoutAndShuffle()
    {
        blackoutCanvasPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        ShuffleObjects();
        yield return new WaitForSeconds(2.5f);

        blackoutCanvasPanel.SetActive(false);
    }

    private void ShuffleObjects()
    {
        List<GameObject> slotObjects = new List<GameObject>();

        slotObjectsOrderBeforeShuffling.Clear();

        foreach (GameObject slot in slots)
        {
            slotObjectsOrderBeforeShuffling.Add(slot.transform.GetChild(0).gameObject);
            slotObjects.Add(slot.transform.GetChild(0).gameObject);
        }

        // Fisher-Yates shuffle
        for (int i = slotObjects.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (slotObjects[i], slotObjects[j]) = (slotObjects[j], slotObjects[i]);
        }

        for (int i = 0; i < slots.Length; i++)
        {
            slotObjects[i].transform.SetParent(slots[i].transform, false);
        }
    }


    public void SwitchObjects()
    {
        if (isSwitching)
            return;
        
        isSwitching = true;

        GameObject firstObject = slotsPickedToPlay[0].transform.GetChild(0).gameObject;
        GameObject secondObject = slotsPickedToPlay[1].transform.GetChild(0).gameObject;

        firstObject.transform.parent = slotsPickedToPlay[1].transform;
        firstObject.transform.position = slotsPickedToPlay[1].transform.position;

        secondObject.transform.parent = slotsPickedToPlay[0].transform;
        secondObject.transform.position = slotsPickedToPlay[0].transform.position;

        Array.Clear(slotsPickedToPlay, 0, slotsPickedToPlay.Length);

        //Reset values
        wasItTheFirstSlotObjectTouched = true;
        isSwitching = false;

        isSlotObjectSequenceCorrect = CheckIfItsTheCorrectOrder();
        print(isSlotObjectSequenceCorrect);
    }

    private bool CheckIfItsTheCorrectOrder()
    {
        bool isSequenceCorrect = true;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.GetChild(0).gameObject != slotObjectsOrderBeforeShuffling[i])
            {
                isSequenceCorrect = false;
                break;
            }
        }

        return isSequenceCorrect;
    }

}
