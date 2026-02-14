using UnityEngine;

public class HeavyBreathingEffect : MonoBehaviour
{
    private ObservableObject obj;
    [SerializeField] private AudioSource breathingSound;
    private bool isBreathingActive;

    private void Awake()
    {
        obj = GetComponent<ObservableObject>();
    }

    void Update()
    {
        if (obj.Stability <= 0.6f && isBreathingActive == false)
        {
            breathingSound.loop = true;
            breathingSound.Play();
            isBreathingActive = true;
        }
        else if (obj.Stability > 0.6f && isBreathingActive)
        {
            breathingSound.Stop();
            isBreathingActive = false;
        }
    }
}
