using UnityEngine;

public class ColorButtonScript : MonoBehaviour
{
    private Camera playerCamera;
    private Renderer objRenderer;
    [SerializeField] private AudioSource colorButtonSound;
    [SerializeField] private ColorPatternTestScript colorPatternTestScript;

    private void Awake()
    {
        playerCamera = Camera.main;
        objRenderer = GetComponent<Renderer>();
        objRenderer.material.DisableKeyword("_EMISSION");
    }

    private void Start()
    {
        colorPatternTestScript.isPlayerAllowedToPlay = false;
    }

    void Update()
    {
        if (!colorPatternTestScript.isPlayerAllowedToPlay)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            LayerMask layer = ~LayerMask.GetMask("Ignore Raycast");

            if (Physics.Raycast(ray, out RaycastHit hit, 10f, layer))
            {
                if (hit.collider.gameObject != gameObject)
                    return;

                if (IsEmissionActive()) //If emission is enabled, do nothing
                    return;

                Activate();
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

    public bool IsEmissionActive()
    {
        return objRenderer.material.IsKeywordEnabled("_EMISSION");
    }
}
