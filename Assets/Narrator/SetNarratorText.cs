using UnityEngine;
using TMPro;
using System.Collections;

public class SetNarratorText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        SetText("");
    }

    void Start()
    {
        StartCoroutine(WaitAndSendFirstNarratorMessage());
    }

    private IEnumerator WaitAndSendFirstNarratorMessage()
    {
        yield return new WaitForSeconds(5f); 
        SetText("Hello, when you are ready, please proceed to the next room.");
        yield return new WaitForSeconds(4f);
        SetText("");
    }

    private void SetText(string textToDisplay)
    {
        text.text = textToDisplay;
    }
}
