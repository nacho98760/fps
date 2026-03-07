using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UIElements;

public class NarrativeManager : MonoBehaviour
{
    public event Action<string> OnNarrativeEventTriggered;

    private PlayerMovement playerScript;
    private bool wasFirstVariantOfFirstTestTriggered;
    private bool wasSecondVariantOfFirstTestTriggered;

    [SerializeField] private List<NarrativeEvent> events;
    private Dictionary<string, NarrativeEvent> eventMap;

    [SerializeField] SetNarratorText narratorTextScript;
    [SerializeField] private AudioSource narratorVoice;

    // -----Test rooms scripts-----
    [SerializeField] private ColorPatternTestScript colorPatternTestScript;

    private void Awake()
    {
        playerScript = FindFirstObjectByType<PlayerMovement>();

        eventMap = new Dictionary<string, NarrativeEvent>(); //Ev is short for "Event"
        foreach (NarrativeEvent ev in events)
        {
            eventMap[ev.eventName] = ev;
        }
    }

    private void Start()
    {
        TriggerEvent("Player Spawn Event");
    }

    private void Update()
    {
        if (playerScript.playerCurrentRoom == "Room2" && !wasFirstVariantOfFirstTestTriggered)
        {
            wasFirstVariantOfFirstTestTriggered = true;
            TriggerEvent("First variant of ColorPatternTest");
        }

        if (colorPatternTestScript.didFirstMinigameVariantFinished && !wasSecondVariantOfFirstTestTriggered)
        {
            wasSecondVariantOfFirstTestTriggered = true;
            TriggerEvent("Second variant of ColorPatternTest");
        }
    }

    public void TriggerEvent(string eventName)
    {
        if (eventMap.TryGetValue(eventName, out NarrativeEvent ev))
        {
            StartCoroutine(PlayEvent(ev));
        }
    }

    private IEnumerator PlayEvent(NarrativeEvent narrativeEvent)
    {
        yield return new WaitForSeconds(narrativeEvent.delayBefore);
        OnNarrativeEventTriggered?.Invoke(narrativeEvent.eventName);

        StartCoroutine(narratorTextScript.SetTextDialogue(narrativeEvent.narratorLine, wpm:50f)); 

        //narratorVoice.PlayOneShot(narrativeEvent.narratorClip);
        yield return new WaitForSeconds(narrativeEvent.delayAfter);
    }
}


[System.Serializable]
public class NarrativeEvent
{
    public string eventName;
    public string narratorLine;
    public AudioClip narratorClip;
    public float delayBefore;
    public float delayAfter;
}