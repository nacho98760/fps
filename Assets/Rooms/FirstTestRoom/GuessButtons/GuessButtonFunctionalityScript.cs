using UnityEngine;

public class GuessButtonFunctionalityScript : MonoBehaviour
{
    public GuessButtonScript[] guessButtons;

    private void Start()
    {
        DeactivateEmisionAndTextForAllButtons();
    }

    public void DeactivateEmisionAndTextForAllButtons()
    {
        foreach (GuessButtonScript guessButton in guessButtons)
        {
            guessButton.DeactivateEmission();
            guessButton.guessButtonNumber.text = "";
        }
    }
}
