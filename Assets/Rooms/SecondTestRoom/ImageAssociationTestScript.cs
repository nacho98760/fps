using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAssociationTestScript : MonoBehaviour
{
    [SerializeField] private NarrativeManager narrativeManager;

    [SerializeField] private Renderer screenRenderer;
    [SerializeField] private Material[] imageMaterials;

    bool isOnCooldown = false;

    private void Awake()
    {
        narrativeManager.OnNarrativeEventTriggered += HandleNarrativeEvent;
    }

    private void HandleNarrativeEvent(string eventName, List<string> dialogues)
    {
        if (eventName == "Start of ImageAssociationTest")
        {
            StartCoroutine(PlayMinigame());
        }
    }

    private IEnumerator PlayMinigame()
    {
        yield return new WaitForSeconds(7f);

        foreach (Material imageMaterial in imageMaterials)
        {
            yield return new WaitUntil(() => isOnCooldown == false);
            ShowImage(imageMaterial);
        }
    }

    void ShowImage(Material imageMaterialToShow)
    {
        screenRenderer.material = imageMaterialToShow;
        isOnCooldown = true;
        StartCoroutine(ImageCooldown(2f));
    }

    private IEnumerator ImageCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
}
