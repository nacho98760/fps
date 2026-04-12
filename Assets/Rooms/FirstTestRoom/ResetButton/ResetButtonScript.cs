using UnityEngine;

public class ResetButtonScript : MonoBehaviour
{
    [SerializeField] private ColorPatternTestScript colorPatternTestScript;
    [SerializeField] private GuessButtonFunctionalityScript guessButtonFunctionalityScript;

    private void OnMouseDown()
    {
        if (colorPatternTestScript.isPlayerAllowedToPlay)
        {
            colorPatternTestScript.numberOfPressedButtons = 0;

            // Reseting the variable's value because of the "inoccent until proven guilty" technique
            colorPatternTestScript.isSequenceRight = true;

            guessButtonFunctionalityScript.DeactivateEmisionAndTextForAllButtons();
        }
    }
}
