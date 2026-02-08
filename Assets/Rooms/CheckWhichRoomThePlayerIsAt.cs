using UnityEngine;

public class CheckWhichRoomThePlayerIsAt : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (transform.root.name != player.GetComponent<PlayerMovement>().playerCurrentRoom)
            {
                player.GetComponent<PlayerMovement>().playerCurrentRoom = transform.root.name;
            }
        }
    }
}
