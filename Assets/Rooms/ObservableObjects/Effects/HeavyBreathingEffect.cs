using UnityEngine;

public class HeavyBreathingEffect : MonoBehaviour
{
    private ObservableObject obj;
    [SerializeField] private AudioSource breathingSound;
    private PlayerMovement playerScript;

    private void Awake()
    {
        obj = GetComponent<ObservableObject>();
        playerScript = FindFirstObjectByType<PlayerMovement>();
    }
    void Start()
    {
        breathingSound.loop = true;
        breathingSound.volume = 0f;
        breathingSound.Play();
    }

    void Update()
    {
        if (obj.transform.parent.name == playerScript.playerCurrentRoom)
        {
            ChooseVolumeBasedOnStability();
        }
    }


    private void ChooseVolumeBasedOnStability()
    {
        if (obj.Stability < 0.2f)
        {
            breathingSound.volume = 1f;
        }
        else if (obj.Stability >= 0.2f && obj.Stability < 0.4f)
        {
            breathingSound.volume = 0.5f;
        }
        else if (obj.Stability >= 0.4f && obj.Stability < 0.75f)
        {
            breathingSound.volume = 0.25f;
        }
        else
        {
            breathingSound.volume = 0f;
        }
    }
}
