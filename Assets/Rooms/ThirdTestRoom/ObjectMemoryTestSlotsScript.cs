using Unity.VisualScripting;
using UnityEngine;

public class ObjectMemoryTestSlotsScript : MonoBehaviour
{
    [SerializeField] private ObjectMemoryTestScript objectMemoryTestScript;

    private float hoveredEmissionIntensity = 0.3f;
    private float selectedEmissionIntensity = 1.5f;

    private GameObject modelInsideObjSlot;

    private void Awake()
    {
        modelInsideObjSlot = transform.GetChild(0).GetChild(0).gameObject;
    }

    private void OnMouseEnter()
    {
        foreach (Material objMaterial in modelInsideObjSlot.GetComponent<Renderer>().materials)
        {
            ActivateEmission(objMaterial, hoveredEmissionIntensity);
        }
    }

    private void OnMouseExit()
    {
        foreach (Material objMaterial in modelInsideObjSlot.GetComponent<Renderer>().materials)
        {
            DeactivateEmission(objMaterial);
        }
    }

    private void OnMouseDown()
    {
        if (objectMemoryTestScript.isPlayerAllowedToPlay && objectMemoryTestScript.isBlackoutActive == false)
        {

            // Check this (If player selects the object and then pulls the cursor away from it, the object emission will deactivate even though it wasnt the hoverEmission)
            /*
            foreach (Material objMaterial in modelInsideObjSlot.GetComponent<Renderer>().materials)
            {
                ActivateEmission(objMaterial, hoveredEmissionIntensity);
            }
            */

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