using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAssociationTestScript : MonoBehaviour
{
    [SerializeField] private NarrativeManager narrativeManager;

    [SerializeField] private ImageAssociationTestOptionButtonScript[] optionButtonScripts;

    [SerializeField] private Renderer screenRenderer;
    [SerializeField] private Material defaultScreenMaterial;
    [SerializeField] private Material[] imageMaterials;

    [SerializeField] private Material imageOptionButtonMaterial;

    [NonSerialized] public bool didPlayerPickAnOption = true;
    [NonSerialized] public bool isPlayerAbleToPlay = false;

    string[] textsForParkImage = { "Park", "Tree", "Calm", "Green" };
    string[] textsForCityImage = { "Lights", "City", "Bridge", "People" };
    string[] textsForRoomImage = { "Cozy", "Wood", "Carpet", "Plant" };

    private void Awake()
    {
        narrativeManager.OnNarrativeEventTriggered += HandleNarrativeEvent;
    }

    private void Start()
    {
        TurnOffScreen();
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
        yield return new WaitForSeconds(9.5f);

        foreach (ImageAssociationTestOptionButtonScript optionButton in optionButtonScripts)
        {
            optionButton.imageButtonRendererAssociatedWithThis.material = imageOptionButtonMaterial;
        }

        foreach (Material imageMaterial in imageMaterials)
        {
            yield return new WaitUntil(() => didPlayerPickAnOption);
            ShowImageAndTextOptions(imageMaterial);
        }

        if (didPlayerPickAnOption)
        {
            TurnOffScreen();
        }
    }

    void ShowImageAndTextOptions(Material imageMaterialToShow)
    {
        didPlayerPickAnOption = false;
        isPlayerAbleToPlay = true;
        screenRenderer.material = imageMaterialToShow;
        SetTextOptions(imageMaterialToShow);
    }

    void SetTextOptions(Material imageMaterialToShow)
    {
        string[] textOptionsChosen = ChooseOptions(imageMaterialToShow.name);

        int counter = 0;

        foreach (ImageAssociationTestOptionButtonScript optionButton in optionButtonScripts)
        {
            optionButton.textAssociatedWithThisButton.text = textOptionsChosen[counter];
            counter++;
        }
    }

    string[] ChooseOptions(string imageName)
    {
        string[] textOptionsChosen = null;

        switch (imageName)
        {
            case "Park":
                textOptionsChosen = textsForParkImage;
                break;

            case "City":
                textOptionsChosen = textsForCityImage;
                break;

            case "Room":
                textOptionsChosen = textsForRoomImage;
                break;
        }

        return textOptionsChosen;
    }

    private void TurnOffScreen()
    {
        screenRenderer.material = defaultScreenMaterial;
        foreach (ImageAssociationTestOptionButtonScript optionButton in optionButtonScripts)
        {
            optionButton.textAssociatedWithThisButton.text = "";
            optionButton.imageButtonRendererAssociatedWithThis.material = defaultScreenMaterial;
        }
    }
}
