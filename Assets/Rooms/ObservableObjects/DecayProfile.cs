using UnityEngine;

[CreateAssetMenu(fileName = "DecayProfile", menuName = "Scriptable Objects/DecayProfile")]
public class DecayProfile : ScriptableObject
{
    public float maxStability;
    public float minStability;

    public float decayRate;
    public float recoveryRate;

    public float decayDelay;
}
