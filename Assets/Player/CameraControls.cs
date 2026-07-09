using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private MainMenuScript mainMenuScript;

    float sensX = 200f;
    float sensY = 200f;

    public PlayerMovement player;

    float xRotation;
    float yRotation;

    bool mouseInitialized = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        xRotation = transform.localEulerAngles.x;
        yRotation = transform.eulerAngles.y;
    }

    //Adding onFocus in case user tabs out the game and back, so cursor and camera angle are re-positioned correctly
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus )
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mouseInitialized = false; // re-sync mouse
        }
    }

    private void Update()
    {
        if (!mouseInitialized)
        {
            mouseInitialized = true;
            return; // skip first frame
        }

        if (mainMenuScript.mainMenuEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

            yRotation += mouseX * Time.deltaTime;
            xRotation -= mouseY * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -75f, 75f);

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
            player.gameObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

}