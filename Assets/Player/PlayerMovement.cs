using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public string playerCurrentRoom;
    [SerializeField] Transform firstRoomSpawnPoint;

    public CameraControls playerCameraControls;
    public Camera playerCamera;

    [SerializeField] private AudioSource footstepsSound;

    float movementSpeed = 1.7f;

    bool isGrounded;

    bool isPlayerMoving;

    public float playerHeight;
    public LayerMask solidObjectLayer;

    public float groundDrag;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody playerRigidBody;


    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    private void Start()
    {
        playerCurrentRoom = "Room1";
        playerRigidBody = GetComponent<Rigidbody>();
        playerRigidBody.freezeRotation = true;
        transform.position = firstRoomSpawnPoint.position;
    }


    private void Update()
    {
        MovePlayer();
        SpeedControl();
        CheckForWalkingSound();
    }

    private void FixedUpdate()
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

        movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        playerRigidBody.AddForce(movementDirection * movementSpeed * 25f, ForceMode.Acceleration);

        //Keep an eye on this
        if (isGrounded && movementDirection.sqrMagnitude < 0.01f)
        {
            playerRigidBody.linearVelocity = new Vector3(0f, playerRigidBody.linearVelocity.y, 0f);
        }
    }


    // ----------------------------- Player Movement - START -----------------------------
    private void MovePlayer()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        //Extra fall gravity tp make the jump less smooth
        if (playerRigidBody.linearVelocity.y < 0)
        {
            playerRigidBody.AddForce(Vector3.down * 2.5f, ForceMode.Acceleration);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(playerRigidBody.linearVelocity.x, 0f, playerRigidBody.linearVelocity.z);

        if (flatVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
            playerRigidBody.linearVelocity = new Vector3(limitedVelocity.x, playerRigidBody.linearVelocity.y, limitedVelocity.z);
        }

        isPlayerMoving = (flatVelocity.magnitude > 0.1f);
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
}
