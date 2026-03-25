using UnityEngine;

public class ObjectMemoryTestSlotsScript : MonoBehaviour
{
    [SerializeField] private ObjectMemoryTestScript objectMemoryTestScript;

    private Material defaultFloorFoamMaterial;
    private Material floorFoamMaterialWhileSelected;

    private Material defaultWallFoamMaterial;
    private Material wallFoamMaterialWhileSelected;

    private Material defaultUpperWallFoamMaterial;
    private Material upperWallFoamMaterialWhileSelected;

    private void Awake()
    {
        defaultFloorFoamMaterial = objectMemoryTestScript.defaultFloorFoamMaterial;
        floorFoamMaterialWhileSelected = objectMemoryTestScript.floorFoamMaterialWhileSelected;

        defaultWallFoamMaterial = objectMemoryTestScript.defaultWallFoamMaterial;
        wallFoamMaterialWhileSelected = objectMemoryTestScript.wallFoamMaterialWhileSelected;

        defaultUpperWallFoamMaterial = objectMemoryTestScript.defaultUpperWallFoamMaterial;
        upperWallFoamMaterialWhileSelected = objectMemoryTestScript.upperWallFoamMaterialWhileSelected;
    }

    private void Start()
    {
        SetFoamMaterialsToDefault();
    }

    private void OnMouseEnter()
    {
        print("Hi");
        SetFoamMaterialsToSelected();
    }

    private void OnMouseExit()
    {
        print("BYe");
        SetFoamMaterialsToDefault();
    }


    private void SetFoamMaterialsToDefault()
    {
        foreach (MeshRenderer slotPartRenderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            if (slotPartRenderer.material.name.Contains(defaultFloorFoamMaterial.name))
            {
                slotPartRenderer.material = defaultFloorFoamMaterial;
            }

            else if (slotPartRenderer.material.name.Contains(defaultWallFoamMaterial.name))
            {
                slotPartRenderer.material = defaultWallFoamMaterial;
            }

            else if (slotPartRenderer.material.name.Contains(defaultUpperWallFoamMaterial.name))
            {
                slotPartRenderer.material = defaultUpperWallFoamMaterial;
            }
        }
    }


    private void SetFoamMaterialsToSelected()
    {
        foreach (MeshRenderer slotPartRenderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            if (slotPartRenderer.material.name.Contains(defaultFloorFoamMaterial.name))
            {
                slotPartRenderer.material = floorFoamMaterialWhileSelected;
            }

            else if (slotPartRenderer.material.name.Contains(defaultWallFoamMaterial.name))
            {
                slotPartRenderer.material = wallFoamMaterialWhileSelected;
            }

            else if (slotPartRenderer.material.name.Contains(defaultUpperWallFoamMaterial.name))
            {
                slotPartRenderer.material = upperWallFoamMaterialWhileSelected;
            }
        }
    }
}