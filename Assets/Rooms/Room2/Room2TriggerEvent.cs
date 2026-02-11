using UnityEngine;

public class Room2Script : MonoBehaviour
{
    bool didRoom2DoorEventHappened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (didRoom2DoorEventHappened == false)
            {
                didRoom2DoorEventHappened = true;
                Transform room2 = transform.parent;
                GameObject room2Door = room2.Find("Door").gameObject;

                if (room2Door.GetComponent<CheckForOpenOrClose>().isDoorClosed == false)
                {
                    room2Door.GetComponent<CheckForOpenOrClose>().isDoorClosed = true;
                    room2Door.GetComponent<AudioSource>().Play();
                }
            }
        }
    }
}
