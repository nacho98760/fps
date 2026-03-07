using System;
using System.Collections;
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

    private void HandleNarrativeEvent(string eventName)
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
        didFirstMinigameVariantFinished = true;
        numberOfPressedButtons = 0;
        isPlayerAllowedToPlay = false;

        if (isButtonSequenceRight)
        {
            print("You got it right! Well done.");
        }
        else
        {
            print("Not quite the right sequence, but don't worry, we are not measuring results, only the way you answer.");
        }
    }


    private IEnumerator PlayMinigame(int variant)
    {
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
