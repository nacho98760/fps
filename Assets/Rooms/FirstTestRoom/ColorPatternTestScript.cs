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

    [NonSerialized] public bool isPlayerAllowedToPlay;

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
        if (eventName == "Player Enters First Test Room")
        {
            StartCoroutine(PlayFirstMinigame());
        }
    }

    public void ReportEndOfMinigame(bool isButtonSequenceRight)
    {
        print(isButtonSequenceRight);
    }


    private IEnumerator PlayFirstMinigame()
    {
        buttonsPickedForMinigame = new ColorButtonScript[buttonCountForMinigame];
        
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < buttonCountForMinigame; i++)
        {
            ColorButtonScript randomButton = buttons[Random.Range(0, buttons.Length)];
            buttonsPickedForMinigame[i] = randomButton;

            randomButton.Activate();
            yield return new WaitForSeconds(0.5f);
            randomButton.Deactivate();
            yield return new WaitForSeconds(0.85f);
        }

        isPlayerAllowedToPlay = true;
    }
}
