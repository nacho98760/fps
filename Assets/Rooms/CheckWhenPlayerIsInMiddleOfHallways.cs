using UnityEngine;

public class CheckWhenPlayerIsInMiddleOfHallways : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerScript;
    [SerializeField] private NarrativeManager narrativeManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (transform.root.name == "Hallway1")
            {
                playerScript.playerCurrentRoom = transform.root.name;
                narrativeManager.TriggerEvent("Middle of Hallway 1");
            }
            else if (transform.root.name == "Hallway2")
            {
                playerScript.playerCurrentRoom = transform.root.name;
                narrativeManager.TriggerEvent("Middle of Hallway 2");
            }

            else if (transform.root.name == "Hallway3")
            {
                playerScript.playerCurrentRoom = transform.root.name;
                narrativeManager.TriggerEvent("Middle of Hallway 3");
            }
        }
    }
}
