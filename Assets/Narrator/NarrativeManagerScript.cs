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
    [SerializeField] private ColorPatternTestScript colorPatternTestScript;
    [SerializeField] private ImageAssociationTestScript imageAssociationTestScript;
    [SerializeField] private ObjectMemoryTestScript objectMemoryTestScript;

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
        //StartCoroutine(GameSequence());
        StartCoroutine(GameSequenceUsedForTesting());
    }

    private IEnumerator GameSequenceUsedForTesting()
    {
        yield return StartCoroutine(TriggerEventAndWait("First variant of ColorPatternTest"));

        yield return new WaitUntil(() => colorPatternTestScript.didFirstMinigameVariantFinished);

        string firstResultEvent = colorPatternTestScript.isSequenceRight ? "Minigame Success" : "Minigame Failure";
        yield return StartCoroutine(TriggerEventAndWait(firstResultEvent));

        yield return StartCoroutine(TriggerEventAndWait("Second variant of ColorPatternTest"));

        yield return new WaitUntil(() => colorPatternTestScript.didSecondMinigameVariantFinished);

        string secondResultEvent = colorPatternTestScript.isSequenceRight ? "Minigame Success" : "Minigame Failure";
        yield return StartCoroutine(TriggerEventAndWait(secondResultEvent));

        yield return StartCoroutine(TriggerEventAndWait("End of ColorPatternTest"));
    }


    private IEnumerator GameSequence()
    {
        yield return StartCoroutine(TriggerEventAndWait("Player Spawn Event"));

        yield return new WaitUntil(() => playerScript.playerCurrentRoom == "Room2");

        yield return StartCoroutine(TriggerEventAndWait("First variant of ColorPatternTest"));

        yield return new WaitUntil(() => colorPatternTestScript.didFirstMinigameVariantFinished);

        string firstResultEvent = colorPatternTestScript.isSequenceRight ? "Minigame Success" : "Minigame Failure";
        yield return StartCoroutine(TriggerEventAndWait(firstResultEvent));

        yield return StartCoroutine(TriggerEventAndWait("Second variant of ColorPatternTest"));

        yield return new WaitUntil(() => colorPatternTestScript.didSecondMinigameVariantFinished);

        string secondResultEvent = colorPatternTestScript.isSequenceRight ? "Minigame Success" : "Minigame Failure";
        yield return StartCoroutine(TriggerEventAndWait(secondResultEvent));

        yield return StartCoroutine(TriggerEventAndWait("End of ColorPatternTest"));

        yield return new WaitUntil(() => playerScript.playerCurrentRoom == "Room3");

        yield return StartCoroutine(TriggerEventAndWait("Start of ImageAssociationTest"));

        for (int i = 0; i < imageAssociationTestScript.numberOfImagesToShow; i++)
        {
            yield return new WaitUntil(() => imageAssociationTestScript.didPlayerPickAnOption);

            imageAssociationTestScript.didPlayerPickAnOption = false;
            yield return StartCoroutine(TriggerEventAndWait("In-between association images dialogue"));
        }

        yield return StartCoroutine(TriggerEventAndWait("End of ImageAssociationTest"));

        yield return new WaitUntil(() => playerScript.playerCurrentRoom == "Room4");

        yield return StartCoroutine(TriggerEventAndWait("First variant of ObjectMemoryTest"));
        yield return StartCoroutine(TriggerEventAndWait("Advice on cameras"));
        yield return StartCoroutine(TriggerEventAndWait("Blackout"));
        yield return new WaitUntil(() => objectMemoryTestScript.isSlotObjectSequenceCorrect);

        yield return StartCoroutine(TriggerEventAndWait("End of ObjectMemoryTest Round"));

        yield return StartCoroutine(TriggerEventAndWait("Second variant of ObjectMemoryTest"));
        yield return StartCoroutine(TriggerEventAndWait("Blackout"));
        yield return new WaitUntil(() => objectMemoryTestScript.isSlotObjectSequenceCorrect);

        yield return StartCoroutine(TriggerEventAndWait("End of ObjectMemoryTest Round"));

        yield return StartCoroutine(TriggerEventAndWait("Third variant of ObjectMemoryTest"));
        yield return StartCoroutine(TriggerEventAndWait("Blackout"));
        yield return new WaitUntil(() => objectMemoryTestScript.isSlotObjectSequenceCorrect);

        yield return StartCoroutine(TriggerEventAndWait("End of ObjectMemoryTest Round"));
    }


    private IEnumerator TriggerEventAndWait(string eventName)
    {
        if (!eventMap.TryGetValue(eventName, out NarrativeEvent ev)) 
            yield break;

        yield return new WaitForSeconds(ev.delayBefore);

        OnNarrativeEventTriggered?.Invoke(ev.eventName, ev.dialogues);

        //----------Disabled when testing----------
        //PlayNarratorVoiceOnAllSpeakers(ev.narratorClip);

        yield return StartCoroutine(narratorTextScript.WaitForDialogueToFinish());

        yield return new WaitForSeconds(ev.delayAfter);
    }

    // Still available for one-off triggers that dont need to be awaited
    public void TriggerEvent(string eventName)
    {
        if (eventMap.TryGetValue(eventName, out NarrativeEvent ev))
            StartCoroutine(TriggerEventAndWait(eventName));
    }


    private void PlayNarratorVoiceOnAllSpeakers(AudioClip dialogueToPlay)
    {
        foreach (NarratorVoiceScript narratorVoice in NarratorVoiceScript.narratorVoiceGameObjs)
        {
            narratorVoice.audioSource.PlayOneShot(dialogueToPlay);
        }
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