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
    float jumpForce = 4f;

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
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, solidObjectLayer);

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
        CheckIfPlayerIsSprinting(isGrounded);
        ManagePlayerMovespeed();
        CheckForWalkingSound(isGrounded);
        ManageCameraFOV();
        ManageFootstepsVolume();
        ManageFootstepsSound();
    }

    private void FixedUpdate()
    {
        movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        playerRigidBody.AddForce(movementDirection.normalized * movementSpeed, ForceMode.VelocityChange);
    }


    // ----------------------------- Player Movement - START -----------------------------
    private void MovePlayer(bool isGrounded)
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

    private void CheckIfPlayerIsSprinting(bool isGrounded)
    {
        if (Input.GetKey(KeyCode.LeftShift) && isPlayerMoving && isGrounded && CanPlayerSprint())
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

    private void ManagePlayerMovespeed()
    {
        float desiredSpeed = isPlayerSprinting ? 4f : 2f;
        movementSpeed = Mathf.Lerp(movementSpeed, desiredSpeed, 10f * Time.deltaTime);
    }
    // ----------------------------- Player Movement - END -----------------------------


    private void CheckForWalkingSound(bool isGrounded)
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
