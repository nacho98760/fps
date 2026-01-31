using UnityEngine;

public class CameraControls : MonoBehaviour
{

    float sensX = 200f;
    float sensY = 200f;

    public PlayerMovement player;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

        yRotation += mouseX * Time.deltaTime;
        xRotation -= mouseY * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -75f, 75f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        player.gameObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}