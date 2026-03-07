using UnityEngine;
using TMPro;
using System.Collections;

public class SetNarratorText : MonoBehaviour
{
    [SerializeField] private GameObject textPanel;
    [SerializeField] private TMP_Text narratorText;

    private int activeDialogues = 0;

    private void Awake()
    {
        StartCoroutine(SetTextDialogue("", wpm:50f));
    }

    private void Start()
    {
        print("Helo");
        textPanel.SetActive(false);
    }

    public IEnumerator SetTextDialogue(string textToDisplay, float wpm) //wpm short for "Words per min"
    {
        activeDialogues++;

        textPanel.SetActive(true);
        narratorText.text = "";

        foreach (char letter in textToDisplay)
        {
            narratorText.text += letter;
            yield return new WaitForSeconds(1f / wpm);
        }

        yield return new WaitForSeconds(5f);

        activeDialogues--;

        if (activeDialogues == 0) //If more than 0, it means that the function was called again, meaning another text popped up. So we only close the text UI if its 0 (aka, no more text showed)
            textPanel.SetActive(false);
    }
}
