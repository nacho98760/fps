using UnityEngine;
using TMPro;
using System.Collections;

public class SetNarratorText : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(SetTextDialogue("", 50f));
    }

    public IEnumerator SetTextDialogue(string textToDisplay, float typyingSpeed)
    {
        TMP_Text narratorText = GetComponent<TMP_Text>();
        narratorText.text = "";

        foreach (char letter in textToDisplay)
        {
            narratorText.text += letter;
            yield return new WaitForSeconds(1f / typyingSpeed);
        }
    }
}
