using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorPatternTestScript : MonoBehaviour
{
    [SerializeField] private ColorButtonScript[] buttons;
    [SerializeField] private NarrativeManager narrativeManager;

    public bool isPlayerAllowedToPlay;

    private void Awake()
    {
        narrativeManager.OnNarrativeEventTriggered += HandleNarrativeEvent;
    }

    private void HandleNarrativeEvent(string eventName)
    {
        if (eventName == "Player Enters First Test Room")
        {
            StartCoroutine(PlayFirstMinigame());
        }
    }
    public IEnumerator PlayFirstMinigame()
    {
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < 3; i++)
        {
            ColorButtonScript randomButton = buttons[Random.Range(0, buttons.Length)];

            randomButton.Activate();

            yield return new WaitForSeconds(0.5f);

            randomButton.Deactivate();

            yield return new WaitForSeconds(0.85f);
        }

        isPlayerAllowedToPlay = true;
    }
}
