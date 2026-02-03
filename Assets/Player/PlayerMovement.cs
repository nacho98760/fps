using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;
using static UnityEditor.Progress;

public class PlayerMovement : MonoBehaviour
{
    public CameraControls playerCameraControls;
    public Camera playerCamera;

    [SerializeField] private AudioSource footstepsSound;
    [SerializeField] private AudioResource footstepsWhileWalking;
    [SerializeField] private AudioResource footstepsWhileSprinting;

    float movementSpeed = 2f;
    float jumpForce = 3f;

    bool isGrounded;

    float jumpCooldown = 1f;
    float nextJumpTime = 0f;

    bool isPlayerMoving;
    bool isPlayerSprinting;

    float sprintButtonCooldown = 1f; //Button cooldown so the player cant spam Walking/Sprinting
    float nextSprintButtonTime = 1f; // When can the player activate the sprint button again

    public float playerHeight;
    public LayerMask solidObjectLayer;

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
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, solidObjectLayer);

        if (isGrounded)
        {
            playerRigidBody.linearDamping = groundDrag;
        }
        else
        {
            playerRigidBody.linearDamping = 0f;
        }

        MovePlayer();
        speedControl();
        CheckIfPlayerIsSprinting();
        ManagePlayerMovespeed();
        CheckForWalkingSound();
        ManageCameraFOV();
        ManageFootstepsVolume();
        ManageFootstepsSound();
    }

    private void FixedUpdate()
    {
        movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        playerRigidBody.AddForce(movementDirection * movementSpeed * 25f, ForceMode.Acceleration);

        //Keep an eye on this
        if (isGrounded && movementDirection.sqrMagnitude < 0.01f)
        {
            Vector3 v = playerRigidBody.linearVelocity;
            playerRigidBody.linearVelocity = new Vector3(0f, v.y, 0f);
        }
    }


    // ----------------------------- Player Movement - START -----------------------------
    private void MovePlayer()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && CanPlayerJump())
        {
            Jump();
        }
        
        //Extra fall gravity tp make the jump less smooth
        if (playerRigidBody.linearVelocity.y < 0)
        {
            playerRigidBody.AddForce(Vector3.down * 2.5f, ForceMode.Acceleration);
        }
    }

    private void Jump()
    {
        playerRigidBody.linearVelocity = new Vector3(playerRigidBody.linearVelocity.x, 0f, playerRigidBody.linearVelocity.z);

        playerRigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        nextJumpTime = Time.time + jumpCooldown;
    }

    private bool CanPlayerJump()
    {
        return Time.time >= nextJumpTime;
    }


    private void speedControl()
    {
        Vector3 flatVelocity = new Vector3(playerRigidBody.linearVelocity.x, 0f, playerRigidBody.linearVelocity.z);

        if (flatVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
            playerRigidBody.linearVelocity = new Vector3(limitedVelocity.x, playerRigidBody.linearVelocity.y, limitedVelocity.z);
        }

        isPlayerMoving = (flatVelocity.magnitude > 0.1f);
    }

    private void CheckIfPlayerIsSprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isPlayerMoving && isGrounded && CanPlayerSprint() && isPlayerWalkingForward())
        {
            isPlayerSprinting = true;
            nextSprintButtonTime = Time.time + sprintButtonCooldown;
        }
        else
        {
            isPlayerSprinting = false;
        }
    }

    private bool CanPlayerSprint()
    {
        if (isPlayerSprinting)
        {
            return true;
        }
        else
        {
            return Time.time >= nextSprintButtonTime;
        }
    }

    private bool isPlayerWalkingForward()
    {
        return verticalInput == 1;
    }

    private void ManagePlayerMovespeed()
    {
        float desiredSpeed = isPlayerSprinting ? 4f : 2f;
        movementSpeed = Mathf.Lerp(movementSpeed, desiredSpeed, 10f * Time.deltaTime);
    }
    // ----------------------------- Player Movement - END -----------------------------


    private void CheckForWalkingSound()
    {
        if (isGrounded && isPlayerMoving)
        {
            if (!footstepsSound.isPlaying)
            {
                footstepsSound.Play();
            }
        }
        else
        {
            footstepsSound.Stop();
        }
    }

    private void ManageCameraFOV()
    {
        //This tells the player: Change from "X" speed (60f) to "Y" speed (75f) in "Z" seconds (10f * Time.deltaTime)
        float desiredFov = isPlayerSprinting ? 75f : 60f;
        playerCamera.fieldOfView = Mathf.Lerp( playerCamera.fieldOfView,desiredFov, 10f * Time.deltaTime);
    }

    private void ManageFootstepsVolume()
    {
        float desiredSound = isPlayerSprinting ? 1f : 0.35f;
        footstepsSound.volume = Mathf.Lerp(footstepsSound.volume, desiredSound, 10f * Time.deltaTime);
    }

    private void ManageFootstepsSound()
    {
        if (isPlayerSprinting)
        {
            footstepsSound.resource = footstepsWhileSprinting;
        }
        else
        {
            footstepsSound.resource = footstepsWhileWalking;
        }
    }
}
