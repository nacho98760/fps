using System.Collections;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private AudioSource shotSound;
    [SerializeField] private AudioClip shotSoundClip;

    [SerializeField] float rifleFireRate = 10f; 

    float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            FireShot();
            nextFireTime = Time.time + 1f / rifleFireRate;
        }
    }

    // Check---
    private void FireShot()
    {
        shotSound.PlayOneShot(shotSoundClip);

        Ray raycast = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(raycast, out RaycastHit hit))
        {
            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
}
