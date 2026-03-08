using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorPatternTestScript : MonoBehaviour
{
    [SerializeField] private ColorButtonScript[] buttons;
    [SerializeField] private NarrativeManager narrativeManager;

    public ColorButtonScript[] buttonsPickedForMinigame;

    [NonSerialized] public int buttonCountForMinigame = 5;
    [NonSerialized] public int numberOfPressedButtons;
    [NonSerialized] public bool isSequenceRight;

    [NonSerialized] public bool didFirstMinigameVariantFinished = false;
    [NonSerialized] public bool didSecondMinigameVariantFinished = false;

    [NonSerialized] public bool isPlayerAllowedToPlay;

    public int minigameVariant = 1;

    private void Awake()
    {
        narrativeManager.OnNarrativeEventTriggered += HandleNarrativeEvent;
    }

    private void Start()
    {
        numberOfPressedButtons = 0;
        isSequenceRight = true;
        isPlayerAllowedToPlay = false;
    }

    private void HandleNarrativeEvent(string eventName, List<string> dialogues)
    {
        if (eventName == "First variant of ColorPatternTest")
        {
            minigameVariant = 1;
            StartCoroutine(PlayMinigame(minigameVariant));
        }

        if (eventName == "Second variant of ColorPatternTest")
        {
            minigameVariant = 2;
            StartCoroutine(PlayMinigame(minigameVariant));
        }
    }

    public void ReportEndOfMinigame(bool isButtonSequenceRight)
    {
        if (minigameVariant == 1)
        {
            didFirstMinigameVariantFinished = true;
            didSecondMinigameVariantFinished = false;
        }
        else if (minigameVariant == 2)
        {
            didSecondMinigameVariantFinished = true;
        }

        numberOfPressedButtons = 0;
        isPlayerAllowedToPlay = false;
    }


    private IEnumerator PlayMinigame(int variant)
    {
        isSequenceRight = true;
        buttonsPickedForMinigame = null;
        buttonsPickedForMinigame = new ColorButtonScript[buttonCountForMinigame];
        
        yield return new WaitForSeconds(7f);

        for (int i = 0; i < buttonCountForMinigame; i++)
        {
            ColorButtonScript randomButton = buttons[Random.Range(0, buttons.Length)];
            buttonsPickedForMinigame[i] = randomButton;

            randomButton.Activate();
            yield return new WaitForSeconds(0.5f);
            randomButton.Deactivate();
            yield return new WaitForSeconds(0.85f);
        }

        if (variant == 2)
        {
            Array.Reverse(buttonsPickedForMinigame);
        }

        isPlayerAllowedToPlay = true;
    }
}
