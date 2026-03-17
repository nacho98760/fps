using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAssociationTestScript : MonoBehaviour
{
    [SerializeField] private NarrativeManager narrativeManager;

    [SerializeField] private ImageAssociationTestOptionButtonScript[] optionButtonScripts;

    [SerializeField] private Renderer mainScreenRenderer;
    [SerializeField] private Renderer secondScreenRenderer;
    [SerializeField] private Material defaultScreenMaterial;

    [NonSerialized] public int numberOfImagesToShow;
    [SerializeField] private Material[] imageMaterials;

    [SerializeField] private Material secondScreenMaterial;
    [SerializeField] private Material imageOptionButtonMaterial;

    [NonSerialized] public bool didPlayerPickAnOption = false;
    [NonSerialized] public bool isPlayerAbleToPlay = false;

    string[] textsForParkImage = { "Park", "Tree", "Calm", "Green" };
    string[] textsForCityImage = { "Lights", "City", "Bridge", "People" };
    string[] textsForRoomImage = { "Cozy", "Wood", "Carpet", "Plant" };

    [NonSerialized] public bool didImageOptionsEnded = false;

    private void Awake()
    {
        narrativeManager.OnNarrativeEventTriggered += HandleNarrativeEvent;

        CountNumberOfImagesToShow();
    }

    private void Start()
    {
        didImageOptionsEnded = false;
        TurnOffScreen();
    }

    private void HandleNarrativeEvent(string eventName, List<string> dialogues)
    {
        if (eventName == "Start of ImageAssociationTest")
        {
            StartCoroutine(PlayMinigame());
        }
    }

    void CountNumberOfImagesToShow()
    {
        numberOfImagesToShow = 0;

        for (int i = 0; i < imageMaterials.Length; i++)
        {
            numberOfImagesToShow++;
        }
    }


    private IEnumerator PlayMinigame()
    {
        yield return new WaitForSeconds(11f);

        bool isItTheFirstImage = true;

        foreach (Material imageMaterial in imageMaterials)
        {
            if (isItTheFirstImage)
            {
                isItTheFirstImage = false;
                TurnOnMinorPartsOfScreen();
                ShowImageAndTextOptions(imageMaterial);
            }
            else
            {
                yield return new WaitUntil(() => didPlayerPickAnOption);

                didPlayerPickAnOption = false;
                yield return new WaitForSeconds(0.5f);

                TurnOffScreen();
                yield return new WaitForSeconds(3f);

                TurnOnMinorPartsOfScreen();
                ShowImageAndTextOptions(imageMaterial);
            }
        }

        didImageOptionsEnded = true;
    }

    void ShowImageAndTextOptions(Material imageMaterialToShow)
    {
        isPlayerAbleToPlay = true;
        mainScreenRenderer.material = imageMaterialToShow;
        SetTextOptions(imageMaterialToShow);
    }

    void TurnOnMinorPartsOfScreen()
    {
        secondScreenRenderer.material = secondScreenMaterial;
        foreach (ImageAssociationTestOptionButtonScript optionButton in optionButtonScripts)
        {
            optionButton.imageButtonRendererAssociatedWithThis.material = imageOptionButtonMaterial;
        }
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

    public void TurnOffScreen()
    {
        mainScreenRenderer.material = defaultScreenMaterial;
        secondScreenRenderer.material = defaultScreenMaterial;
        foreach (ImageAssociationTestOptionButtonScript optionButton in optionButtonScripts)
        {
            optionButton.textAssociatedWithThisButton.text = "";
            optionButton.imageButtonRendererAssociatedWithThis.material = defaultScreenMaterial;
        }
    }
}
