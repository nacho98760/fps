using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public Transform cameraPosition;

    private void Awake()
    {
        transform.position = cameraPosition.position;
    }

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
