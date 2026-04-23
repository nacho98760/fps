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
            print("Yes Player");
            if (transform.root.name != playerScript.playerCurrentRoom)
            {
                print("Yes room");
                playerScript.playerCurrentRoom = transform.root.name;
                print(playerScript.playerCurrentRoom);
            }
        }
    }
}