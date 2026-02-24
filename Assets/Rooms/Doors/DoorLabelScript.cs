using UnityEngine;

public class DoorListener : MonoBehaviour
{
    [SerializeField] private DoorScript doorScript;
    [SerializeField] private GameObject labelColorObj;

    [SerializeField] private Material labelColorWhenClosed;
    [SerializeField] private Material labelColorWhenOpened;

    private PlayerMovement playerScript;

    private void Awake()
    {
        doorScript.OnDoorStateChanged += HandleDoorChanged;
        playerScript = FindFirstObjectByType<PlayerMovement>();
    }


    private void HandleDoorChanged(bool isDoorClosed)
    {
        if (transform.root.name == playerScript.playerCurrentRoom)
        {
            if (isDoorClosed)
            {
                labelColorObj.transform.GetComponent<Renderer>().material = labelColorWhenClosed;
            }
            else
            {
                labelColorObj.transform.GetComponent<Renderer>().material = labelColorWhenOpened;
            }
        }
    }
}