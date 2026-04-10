using TMPro;
using UnityEngine;

public class GuessButtonScript : MonoBehaviour
{
    private Renderer objRenderer;
    public TMP_Text guessButtonNumber;

    private void Awake()
    {
        objRenderer = GetComponent<Renderer>();
    }

    public void ActivateEmission()
    {
        objRenderer.material.EnableKeyword("_EMISSION");
    }

    public void DeactivateEmission()
    {
        objRenderer.material.DisableKeyword("_EMISSION");
    }
}
