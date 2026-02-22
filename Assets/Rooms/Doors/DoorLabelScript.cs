using UnityEngine;

public class DoorListener : MonoBehaviour
{
    [SerializeField] private DoorScript doorScript;
    [SerializeField] private GameObject labelColorObj;

    private void Awake()
    {
        doorScript.OnDoorStateChanged += HandleDoorChanged;
    }


    private void HandleDoorChanged(bool isDoorClosed)
    {
        if (isDoorClosed)
        {
            labelColorObj.transform.GetComponent<Renderer>().material.color = Color.red;
            labelColorObj.transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red * 1.5f);
        }
        else
        {
            labelColorObj.transform.GetComponent<Renderer>().material.color = Color.green;
            labelColorObj.transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green * 1.5f);
        }
    }
}