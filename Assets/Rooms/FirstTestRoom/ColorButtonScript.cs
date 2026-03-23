using System;
using System.Collections;
using UnityEngine;

public class ColorButtonScript : MonoBehaviour
{
    private Renderer objRenderer;
    [SerializeField] private AudioSource colorButtonSound;
    [SerializeField] private ColorPatternTestScript colorPatternScript;

    private bool isButtonOnCooldown = false;

    private void Awake()
    {
        objRenderer = GetComponent<Renderer>();
        objRenderer.material.DisableKeyword("_EMISSION");
    }

    private void OnMouseDown()
    {
        if (colorPatternScript.isPlayerAllowedToPlay)
        {
            CheckPlayersInputOnMinigame();
        }
    }

    private void CheckPlayersInputOnMinigame()
    {
        if (colorPatternScript.numberOfPressedButtons < colorPatternScript.buttonCountForMinigame && !isButtonOnCooldown)
        {
            StartCoroutine(ButtonCooldown());
            colorPatternScript.numberOfPressedButtons++;

            StartCoroutine(FullButtonPressed());

            ColorButtonScript rightSequenceButton = colorPatternScript.buttonsPickedForMinigame[colorPatternScript.numberOfPressedButtons - 1];

            if (gameObject.name != rightSequenceButton.gameObject.name)
            {
                colorPatternScript.isSequenceRight = false;
            }

            if (colorPatternScript.numberOfPressedButtons == colorPatternScript.buttonCountForMinigame)
            {
                colorPatternScript.ReportEndOfMinigame(colorPatternScript.isSequenceRight);
            }
        }
    }

    public void Activate()
    {
        objRenderer.material.EnableKeyword("_EMISSION");
        colorButtonSound.Play();
    }
    public void Deactivate()
    {
        objRenderer.material.DisableKeyword("_EMISSION");
    }

    private IEnumerator FullButtonPressed()
    {
        Activate();
        yield return new WaitForSeconds(0.5f);
        Deactivate();
    }

    public bool IsEmissionActive()
    {
        return objRenderer.material.IsKeywordEnabled("_EMISSION");
    }

    private IEnumerator ButtonCooldown()
    {
        isButtonOnCooldown = true;
        yield return new WaitForSeconds(0.5f);
        isButtonOnCooldown = false;
    }
}
