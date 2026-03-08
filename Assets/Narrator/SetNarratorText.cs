using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetNarratorText : MonoBehaviour
{
    [SerializeField] private NarrativeManager narrativeManager;
    [SerializeField] private GameObject textPanel;
    [SerializeField] private TMP_Text narratorText;

    [NonSerialized] public Queue<string> dialogueQueue = new Queue<string>();
    [NonSerialized] public bool isDialoguePlaying = false;

    private void Awake()
    {
        narrativeManager.OnNarrativeEventTriggered += HandleNarrativeEvent;
    }

    private void Start()
    {
        narratorText.text = "";
        textPanel.SetActive(false);
    }

    public void QueueDialogue(string text, float timeToRead)
    {
        dialogueQueue.Enqueue(text);

        if (!isDialoguePlaying)
            StartCoroutine(PlayNextDialogue(timeToRead, wpm: 50f));
    }

    private IEnumerator PlayNextDialogue(float timeToRead, float wpm)
    {
        isDialoguePlaying = true;

        while (dialogueQueue.Count > 0)
        {
            string text = dialogueQueue.Dequeue();

            textPanel.SetActive(true);
            narratorText.text = "";

            foreach (char letter in text)
            {
                narratorText.text += letter;
                yield return new WaitForSeconds(1f / wpm);
            }

            yield return new WaitForSeconds(timeToRead);
        }

        isDialoguePlaying = false;
        textPanel.SetActive(false);
    }


    public IEnumerator WaitForDialogueToFinish()
    {
        yield return new WaitUntil(() => !isDialoguePlaying && dialogueQueue.Count == 0);
    }

    private void HandleNarrativeEvent(string eventName, List<string> dialogues)
    {
        switch (eventName)
        {
            case "Player Spawn Event":
                QueueDialogue(dialogues[0], 2f);
                break;

            case "First variant of ColorPatternTest":
                QueueDialogue(dialogues[0], 7f);
                break;

            case "Second variant of ColorPatternTest":
                QueueDialogue(dialogues[0], 6f);
                break;

            case "Minigame Success":
                QueueDialogue(dialogues[0], 2f);
                break;

            case "Minigame Failure":
                QueueDialogue(dialogues[0], 2f);
                break;
        }
    }
}
