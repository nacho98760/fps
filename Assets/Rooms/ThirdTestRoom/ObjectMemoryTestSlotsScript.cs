using UnityEngine;

public class ObjectMemoryTestSlotsScript : MonoBehaviour
{
    [SerializeField] private ObjectMemoryTestScript objectMemoryTestScript;

    private void Awake()
    {
        //---
    }

    private void OnMouseEnter()
    {
        print("Hi");
    }

    private void OnMouseExit()
    {
        print("BYe");
    }
}