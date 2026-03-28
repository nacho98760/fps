using System;
using UnityEngine;

public class ObjectMemoryTestScript : MonoBehaviour
{
    [SerializeField] private GameObject[] slots;

    [NonSerialized] public bool isPlayerAllowedToPlay;
    [NonSerialized] public bool wasItTheFirstSlotObjectTouched = true;
    [NonSerialized] public bool didPlayerPick2Objects = false;

    public GameObject[] slotsPickedToPlay;

    private bool isSwitching = false;

    private void Start()
    {
        isPlayerAllowedToPlay = true;
        slotsPickedToPlay = new GameObject[2];
    }

    public void SwitchObjects()
    {
        if (isSwitching)
            return;
        
        isSwitching = true;

        GameObject firstObject = slotsPickedToPlay[0].transform.GetChild(0).gameObject;
        GameObject secondObject = slotsPickedToPlay[1].transform.GetChild(0).gameObject;

        firstObject.transform.parent = slotsPickedToPlay[1].transform;
        firstObject.transform.position = slotsPickedToPlay[1].transform.position;

        secondObject.transform.parent = slotsPickedToPlay[0].transform;
        secondObject.transform.position = slotsPickedToPlay[0].transform.position;

        Array.Clear(slotsPickedToPlay, 0, slotsPickedToPlay.Length);

        wasItTheFirstSlotObjectTouched = true;
        isSwitching = false;
    }
}
