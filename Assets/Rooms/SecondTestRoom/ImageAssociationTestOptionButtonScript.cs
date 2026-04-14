using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ImageAssociationTestOptionButtonScript : MonoBehaviour
{
    [SerializeField] private ImageAssociationTestScript imageAssociationTestScript;

    public TMP_Text textAssociatedWithThisButton;
    public Renderer imageButtonRendererAssociatedWithThis;


    private void OnMouseDown()
    {
        if (imageAssociationTestScript.isPlayerAbleToPlay)
        {
            imageAssociationTestScript.didPlayerPickAnOptionLocal = true;
            imageAssociationTestScript.didPlayerPickAnOptionGlobal = true;

            imageAssociationTestScript.isPlayerAbleToPlay = false;

            if (imageAssociationTestScript.didImageOptionsEnded)
            {
                StartCoroutine(WaitAndTurnOffScreen());
            }
        }
    }

    private IEnumerator WaitAndTurnOffScreen()
    {
        yield return new WaitForSeconds(0.2f);
        imageAssociationTestScript.TurnOffScreen();
    }

}
