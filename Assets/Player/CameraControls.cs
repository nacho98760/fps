using UnityEngine;

public class CameraControls : MonoBehaviour
{

    float sensX = 180f;
    float sensY = 180f;

    public PlayerMovement player;

    float xRotation;
    float yRotation;

    bool mouseInitialized = false;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        xRotation = transform.localEulerAngles.x;
        yRotation = transform.eulerAngles.y;
    }

    //Adding onFocus in case user tabs out the game and back, so cursor and camera angle are re-positioned correctly
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mouseInitialized = false; // re-sync mouse
        }
    }

    void Update()
    {
        if (!mouseInitialized)
        {
            mouseInitialized = true;
            return; // skip first frame
        }

        float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

        yRotation += mouseX * Time.deltaTime;
        xRotation -= mouseY * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -75f, 75f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        player.gameObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

}