using UnityEngine;

public class BehindTheScenesRoomScript : MonoBehaviour
{
    [SerializeField] private Material normalWallMaterial;
    [SerializeField] private Material transparentWallMaterial;

    private Renderer objRenderer;

    private PlayerMovement playerScript;

    private void Awake()
    {
        playerScript = FindFirstObjectByType<PlayerMovement>();
        objRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (playerScript.playerCurrentRoom == "StaffRoom")
        {
            ChangeToTransparent();
        }
        else
        {
            ChangeToNormal();
        }
    }

    void ChangeToTransparent()
    {
        objRenderer.material = transparentWallMaterial;
    }

    void ChangeToNormal()
    {
        objRenderer.material = normalWallMaterial;
    }
}
