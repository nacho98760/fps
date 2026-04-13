using UnityEngine;

public class EnterButtonScript : MonoBehaviour
{
    [SerializeField] private ColorPatternTestScript colorPatternTestScript;

    private void OnMouseDown()
    {
        if (!colorPatternTestScript.isPlayerAllowedToPlay)
            return;

        if (colorPatternTestScript.numberOfPressedButtons == colorPatternTestScript.buttonCountForMinigame)
        {
            colorPatternTestScript.ReportEndOfMinigame(colorPatternTestScript.isSequenceRight);
        }
    }
}
