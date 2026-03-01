using UnityEngine;

public class ColorButtonScript : MonoBehaviour
{
    private Camera playerCamera;
    Renderer objRenderer;
    [SerializeField] private AudioSource colorButtonSound;

    bool isPlayerAllowedToPlay = false;

    private void Awake()
    {
        playerCamera = Camera.main;
        objRenderer = GetComponent<Renderer>();
        objRenderer.material.DisableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void Activate()
    {
        objRenderer.material.EnableKeyword("_EMISSION");
        colorButtonSound.Play();
    }
    void Deactivate()
    {
        objRenderer.material.DisableKeyword("_EMISSION");
    }


    void PlayGuessingGame()
    {
        if (Input.GetMouseButtonDown(0) && isPlayerAllowedToPlay)
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            LayerMask layer = ~LayerMask.GetMask("Ignore Raycast");

            if (Physics.Raycast(ray, out RaycastHit hit, 20f, layer))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (objRenderer.material.IsKeywordEnabled("_EMISSION")) //If emission is enabled, do nothing
                        return;

                    Activate();
                }
            }

        }
    }
}
