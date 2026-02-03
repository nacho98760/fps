using UnityEngine;

public class ObservableObject : MonoBehaviour
{
    public Vector3 objOriginalPosition;
    public Vector3 objFirstStatePosition;
    public bool isItFirstTimeObservation;
    public bool isCurrentlyObserved;
    void Start()
    {
        isItFirstTimeObservation = true;
        objOriginalPosition = transform.position;
        objFirstStatePosition = objOriginalPosition + new Vector3(0f, 0f, 6f);
    }
}
