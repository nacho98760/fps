using UnityEngine;

public class SlotObjectModelScript : MonoBehaviour
{
    [SerializeField] private ObjectMemoryTestScript objectMemoryTestScript;

    private float hoveredEmissionIntensity = 0.035f;
    private float selectedEmissionIntensity = 0.2f;

    public ObjectState objectState;

    private void Start()
    {
        objectState = ObjectState.NotHoveredNorSelected;
    }

    private void Update()
    {
        ChooseEmission();
    }

    private void OnMouseEnter()
    {
        if (objectState == ObjectState.Selected)
            return;

        objectState = ObjectState.Hovered;
    }

    private void OnMouseExit()
    {
        if (objectState == ObjectState.Hovered)
        {
            objectState = ObjectState.NotHoveredNorSelected;
        }
    }

    private void OnMouseDown()
    {
        if (objectMemoryTestScript.isPlayerAllowedToPlay && objectMemoryTestScript.isBlackoutActive == false)
        {
            if (objectState == ObjectState.Selected)
            {
                objectMemoryTestScript.slotsPickedToPlay.Remove(transform.parent.parent.gameObject);
                objectState = ObjectState.Hovered;
                return;
            }

            if (objectMemoryTestScript.slotsPickedToPlay.Count < 2)
            {
                objectState = ObjectState.Selected;
                objectMemoryTestScript.slotsPickedToPlay.Add(transform.parent.parent.gameObject);

                if (objectMemoryTestScript.slotsPickedToPlay.Count == 2)
                {
                    objectMemoryTestScript.SwitchObjects();
                }
            }
        }
    }

    private void ChooseEmission()
    {
        if (objectState == ObjectState.NotHoveredNorSelected)
        {
            ActivateOrDeactivateEmissionOnSlot(false);
        }
        else if (objectState == ObjectState.Hovered)
        {
            ActivateOrDeactivateEmissionOnSlot(true, hoveredEmissionIntensity);
        }
        else if (objectState == ObjectState.Selected)
        {
            ActivateOrDeactivateEmissionOnSlot(true, selectedEmissionIntensity);
        }
    }


    private void ActivateOrDeactivateEmissionOnSlot(bool activate, float intensity = 0f)
    {
        if (TryGetComponent<Renderer>(out Renderer rend))
        {
            foreach (Material objMaterial in GetComponent<Renderer>().materials)
            {
                if (activate)
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
        if (transform.childCount > 0)
        {
            foreach (Renderer modelChildRenderer in GetComponentsInChildren<Renderer>())
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

