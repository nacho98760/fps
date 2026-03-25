using UnityEngine;

public class ObjectMemoryTestScript : MonoBehaviour
{
    [SerializeField] private GameObject[] foamSlots;

    public Material defaultFloorFoamMaterial;
    public Material floorFoamMaterialWhileSelected;

    public Material defaultWallFoamMaterial;
    public Material wallFoamMaterialWhileSelected;

    public Material defaultUpperWallFoamMaterial;
    public Material upperWallFoamMaterialWhileSelected;
}
