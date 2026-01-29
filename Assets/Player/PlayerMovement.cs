using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;
using static UnityEditor.Progress;

public class PlayerMovement : MonoBehaviour
{
    public CameraControls playerCameraControls;

    [SerializeField] private AudioSource walkingSound;

    private bool isPlayerMoving;
    private bool isPlayerShifting;

    float movementSpeed = 2f;
    float jumpForce = 4f;

    public float playerHeight;
    public LayerMask floorLayer;

    public float groundDrag;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody playerRigidBody;

    private void Awake()
    {
        Application.targetFrameRate = 180;
    }

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerRigidBody.freezeRotation = true;
    }


    private void Update()
    {
        print(movementSpeed);
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, floorLayer);

        if (isGrounded)
        {
            playerRigidBody.linearDamping = groundDrag;
        }
        else
        {
            playerRigidBody.linearDamping = 0;
        }

        MovePlayer(isGrounded);
        speedControl();
        CheckForWalkingSound();
    }

    private void FixedUpdate()
    {
        CheckIfPlayerIsShifting();
        movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        playerRigidBody.AddForce(movementDirection.normalized * movementSpeed, ForceMode.VelocityChange);
    }


    // ----------------------------- Player Movement - START -----------------------------
    private void MovePlayer(bool isGrounded)
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        playerRigidBody.linearVelocity = new Vector3(playerRigidBody.linearVelocity.x, 0f, playerRigidBody.linearVelocity.z);

        playerRigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void speedControl()
    {
        Vector3 flatVelocity = new Vector3(playerRigidBody.linearVelocity.x, 0f, playerRigidBody.linearVelocity.z);

        if (flatVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
            playerRigidBody.linearVelocity = new Vector3(limitedVelocity.x, playerRigidBody.linearVelocity.y, limitedVelocity.z);
        }

        isPlayerMoving = flatVelocity.magnitude > 0.1f;
    }

    private void CheckIfPlayerIsShifting()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            movementSpeed = 1f;
            isPlayerShifting = true;
        }
        else
        {
            movementSpeed = 2f;
            isPlayerShifting = false;
        }
    }

    private void CheckForWalkingSound()
    {
        if (isPlayerMoving && !isPlayerShifting)
        {
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
        }
        else
        {
            walkingSound.Stop();
        }
    }
}
