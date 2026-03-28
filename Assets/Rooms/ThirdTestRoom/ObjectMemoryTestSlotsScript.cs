using Unity.VisualScripting;
using UnityEngine;

public class ObjectMemoryTestSlotsScript : MonoBehaviour
{
    [SerializeField] private ObjectMemoryTestScript objectMemoryTestScript;

    private void OnMouseEnter()
    {
        print("Hi");
    }

    private void OnMouseExit()
    {
        print("BYe");
    }

    private void OnMouseDown()
    {
        if (objectMemoryTestScript.isPlayerAllowedToPlay)
        {
            if (objectMemoryTestScript.wasItTheFirstSlotObjectTouched)
            {
                objectMemoryTestScript.wasItTheFirstSlotObjectTouched = false;
                objectMemoryTestScript.slotsPickedToPlay[0] = this.gameObject;
            }
            else
            {
                //If the second slot object picked is the same as the first one, we do nothing
                if (this.gameObject != objectMemoryTestScript.slotsPickedToPlay[0])
                {
                    objectMemoryTestScript.slotsPickedToPlay[1] = this.gameObject;
                    objectMemoryTestScript.SwitchObjects();
                }
            }
        }
    }
}