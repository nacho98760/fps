using System;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectMemoryTestSlotsScript : MonoBehaviour
{
    [SerializeField] private ObjectMemoryTestScript objectMemoryTestScript;

    private float hoveredEmissionIntensity = 0.035f;
    private float selectedEmissionIntensity = 0.2f;

    public ObjectState objectState;

    private void Start()
    {
        objectState = ObjectState.NotHoveredNorSelected;
    }

    private void OnMouseEnter()
    {
        objectState = ObjectState.Hovered;

        ActivateOrDeactivateEmissionOnSlot(true, hoveredEmissionIntensity);
    }

    private void OnMouseExit()
    {
        if (objectState == ObjectState.Hovered)
        {
            ActivateOrDeactivateEmissionOnSlot(false);
        }
    }

    private void OnMouseDown()
    {
        objectState = ObjectState.Selected;

        ActivateOrDeactivateEmissionOnSlot(true, selectedEmissionIntensity);

        if (objectMemoryTestScript.isPlayerAllowedToPlay && objectMemoryTestScript.isBlackoutActive == false)
        {
            if (objectMemoryTestScript.wasItTheFirstSlotObjectTouched)
            {
                objectMemoryTestScript.wasItTheFirstSlotObjectTouched = false;
                objectMemoryTestScript.slotsPickedToPlay[0] = this.gameObject;
            }
            else
            {
                //If the second slot object picked is the same as the first one, we do nothing
                if (this.gameObject != objectMemoryTestScript.slotsPickedToPlay[0])
                {
                    objectMemoryTestScript.slotsPickedToPlay[1] = this.gameObject;
                    objectMemoryTestScript.SwitchObjects();
                }
            }
        }
    }

    private void ActivateOrDeactivateEmissionOnSlot(bool activate, float intensity = 0f)
    {
        GameObject slotObjectModel = gameObject.transform.GetChild(0).GetChild(0).gameObject;

        if (slotObjectModel.TryGetComponent<Renderer>(out Renderer rend))
        {
            foreach (Material objMaterial in slotObjectModel.GetComponent<Renderer>().materials)
            {
                if (activate == true)
                {
                    ActivateEmission(objMaterial, intensity);
                }
                else
                {
                    DeactivateEmission(objMaterial);
                }
            }
        }

        // Double check for obj models which have separate materials inside (Ex. Watch)
        if (slotObjectModel.transform.childCount > 0)
        {
            foreach (Renderer modelChildRenderer in slotObjectModel.GetComponentsInChildren<Renderer>())
            {
                foreach (Material objMaterial in modelChildRenderer.materials)
                {
                    if (activate == true)
                    {
                        ActivateEmission(objMaterial, intensity);
                    }
                    else
                    {
                        DeactivateEmission(objMaterial);
                    }
                }
            }
        }
    }


    private void ActivateEmission(Material objMaterial, float intensity)
    {
        objMaterial.EnableKeyword("_EMISSION");

        Color baseColor = objMaterial.GetColor("_BaseColor");
        objMaterial.SetColor("_EmissionColor", baseColor * intensity);
    }

    private void DeactivateEmission(Material objMaterial)
    {
        objMaterial.DisableKeyword("_EMISSION");
    }
}


public enum ObjectState
{
    NotHoveredNorSelected,
    Hovered,
    Selected,
}