using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UIElements;

public class NarrativeManager : MonoBehaviour
{
    private PlayerMovement playerScript;
    private bool wasEventTriggered;
    private DoorScript doorScript;

    [SerializeField] private List<NarrativeEvent> events;
    private Dictionary<string, NarrativeEvent> eventMap;

    [SerializeField] private TMP_Text narratorText;
    [SerializeField] private AudioSource narratorVoice;

    public event Action<string> OnNarrativeEventTriggered;

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
        if (playerScript.playerCurrentRoom == "Room2" && wasEventTriggered == false)
        {
            wasEventTriggered = true;
            TriggerEvent("Player Enters First Test Room");
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
        narratorText.SetText(narrativeEvent.narratorLine);
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