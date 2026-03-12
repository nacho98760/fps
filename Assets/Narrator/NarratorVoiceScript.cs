using System;
using System.Collections.Generic;
using UnityEngine;

public class NarratorVoiceScript : MonoBehaviour
{
    public static List<NarratorVoiceScript> narratorVoiceGameObjs = new List<NarratorVoiceScript>();

    [NonSerialized] public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        narratorVoiceGameObjs.Add(this);
    }

    private void OnDestroy() => narratorVoiceGameObjs.Remove(this);
}
