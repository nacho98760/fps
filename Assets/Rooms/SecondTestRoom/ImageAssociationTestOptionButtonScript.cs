using System;
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
            print(textAssociatedWithThisButton.text);
            imageAssociationTestScript.didPlayerPickAnOption = true;
            imageAssociationTestScript.isPlayerAbleToPlay = false;
        }
    }

}
