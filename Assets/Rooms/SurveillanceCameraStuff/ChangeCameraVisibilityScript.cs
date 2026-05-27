using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraVisibilityScript : MonoBehaviour
{
    [SerializeField] private NarrativeManager narrativeManager;

    private void Awake()
    {
        narrativeManager.OnNarrativeEventTriggered += HandleNarrativeEvent;
    }


    private void HandleNarrativeEvent(string eventName, List<string> dialogues)
    {
        if (eventName == "Advice on cameras")
        {
            gameObject.SetActive(false);
        }
    }
}
