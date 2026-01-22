using System.Collections;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private AudioSource shotSound;

    void Start()
    {
        
    }

    void Update()
    {
        Ray raycast = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(raycast.origin, raycast.direction * 30f);

        if (Input.GetMouseButton(0))
        {
            FireShot();
            if (Physics.Raycast(raycast, out RaycastHit hit))
            {
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }
    }

    // Check---
    private void FireShot()
    {
        if (!shotSound.isPlaying)
        {
            shotSound.Play();
        }
    }
}
