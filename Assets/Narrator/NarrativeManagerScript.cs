using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class NarrativeManager : MonoBehaviour
{
    public event Action<string, List<string>> OnNarrativeEventTriggered;

    [SerializeField] private List<NarrativeEvent> events;
    [SerializeField] private SetNarratorText narratorTextScript;
    [SerializeField] private AudioSource narratorVoice;
    [SerializeField] private ColorPatternTestScript colorPatternTestScript;

    private PlayerMovement playerScript;
    private Dictionary<string, NarrativeEvent> eventMap;

    private void Awake()
    {
        playerScript = FindFirstObjectByType<PlayerMovement>();

        eventMap = new Dictionary<string, NarrativeEvent>();
        foreach (NarrativeEvent ev in events)
            eventMap[ev.eventName] = ev;
    }

    private void Start()
    {
        StartCoroutine(GameSequence());
    }

    private IEnumerator GameSequence()
    {
        // Player spawns
        yield return StartCoroutine(TriggerEventAndWait("Player Spawn Event"));

        // Wait for player to enter Room2
        yield return new WaitUntil(() => playerScript.playerCurrentRoom == "Room2");

        // First minigame variant
        yield return StartCoroutine(TriggerEventAndWait("First variant of ColorPatternTest"));

        // Wait for first minigame to finish
        yield return new WaitUntil(() => colorPatternTestScript.didFirstMinigameVariantFinished);

        // Show result dialogue and wait for it to finish
        string firstResultEvent = colorPatternTestScript.isSequenceRight ? "Minigame Success" : "Minigame Failure";
        yield return StartCoroutine(TriggerEventAndWait(firstResultEvent));

        // Second minigame variant
        yield return StartCoroutine(TriggerEventAndWait("Second variant of ColorPatternTest"));

        // Wait for second minigame to finish
        yield return new WaitUntil(() => colorPatternTestScript.didSecondMinigameVariantFinished);

        // Show result dialogue and wait for it to finish
        string secondResultEvent = colorPatternTestScript.isSequenceRight ? "Minigame Success" : "Minigame Failure";
        yield return StartCoroutine(TriggerEventAndWait(secondResultEvent));

        // Continue with next events...
    }


    private IEnumerator TriggerEventAndWait(string eventName)
    {
        if (!eventMap.TryGetValue(eventName, out NarrativeEvent ev)) 
            yield break;

        yield return new WaitForSeconds(ev.delayBefore);

        OnNarrativeEventTriggered?.Invoke(ev.eventName, ev.dialogues);
        yield return StartCoroutine(narratorTextScript.WaitForDialogueToFinish());

        yield return new WaitForSeconds(ev.delayAfter);
    }



    // Still available for one-off triggers that don't need to be awaited
    public void TriggerEvent(string eventName)
    {
        if (eventMap.TryGetValue(eventName, out NarrativeEvent ev))
            StartCoroutine(TriggerEventAndWait(eventName));
    }
}



[System.Serializable]
public class NarrativeEvent
{
    public string eventName;
    public List<string> dialogues;
    public AudioClip narratorClip;
    public float delayBefore;
    public float delayAfter;
}