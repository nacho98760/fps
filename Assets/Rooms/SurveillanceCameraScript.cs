using UnityEngine;

public class SurveillanceCameraScript : MonoBehaviour
{
    [SerializeField] private GameObject upperCameraModel;
    [SerializeField] private PlayerMovement playerScript;

    void Update()
    {
        upperCameraModel.transform.LookAt(playerScript.transform);
    }
}
