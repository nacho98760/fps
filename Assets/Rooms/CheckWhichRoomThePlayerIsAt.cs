using UnityEngine;

public class CheckWhichRoomThePlayerIsAt : MonoBehaviour
{
    private PlayerMovement playerScript;

    private void Awake()
    {
        playerScript = FindFirstObjectByType<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (transform.root.name != playerScript.playerCurrentRoom)
            {
                playerScript.playerCurrentRoom = transform.root.name;
            }
        }
    }
}
