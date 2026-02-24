using UnityEngine;
using TMPro;
using System.Collections;

public class SetNarratorText : MonoBehaviour
{
    private bool hasClosedDoor = false;

    [SerializeField] private TMP_Text text;
    [SerializeField] DoorScript firstDoor;
    [SerializeField] DoorScript secondDoor;

    private PlayerMovement playerScript;

    private void Awake()
    {
        playerScript = FindFirstObjectByType<PlayerMovement>();
        SetText("");
    }

    void Start()
    {
        StartCoroutine(WaitAndSendFirstNarratorMessage());
    }

    private IEnumerator WaitAndSendFirstNarratorMessage()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(firstDoor.OpenDoor());
        SetText("Hello, when you are ready, please proceed to the next room.");
        yield return new WaitForSeconds(4f);
        SetText("");
    }

    private void Update()
    {
        if (playerScript.playerCurrentRoom == "Room2" && hasClosedDoor == false)
        {
            hasClosedDoor = true;
            StartCoroutine(WaitAndCloseDoor());
        }
    }

    private IEnumerator WaitAndCloseDoor()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(firstDoor.CloseDoor());
    }

    private void SetText(string textToDisplay)
    {
        text.text = textToDisplay;
    }
}
