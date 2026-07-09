using UnityEngine;
using UnityEngine.UI;

public class SettingsOptionsPanelScript : MonoBehaviour
{
    [SerializeField] private Slider ambientSoundVolumeSlider;
    [SerializeField] private AudioSource ambientSound;

    private void Start()
    {
        ambientSoundVolumeSlider.value = 1;
    } 

    private void Update()
    {
        ambientSound.volume = ambientSoundVolumeSlider.value;
    }
}
