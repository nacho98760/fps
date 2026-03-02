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

    private void SetText(string textToDisplay)
    {
        text.text = textToDisplay;
    }
}
