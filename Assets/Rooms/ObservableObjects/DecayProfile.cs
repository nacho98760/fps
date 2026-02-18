using UnityEngine;

[CreateAssetMenu(fileName = "DecayProfile", menuName = "Scriptable Objects/DecayProfile")]
public class DecayProfile : ScriptableObject
{
    public float maxStability;
    public float minStability;

    public float focusedDecayRate;
    public float nonFocusedDecayRate;

    public float decayDelay;
}
