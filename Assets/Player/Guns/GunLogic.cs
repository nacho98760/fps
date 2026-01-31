using System.Collections;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private AudioSource shotSound;
    [SerializeField] private AudioClip shotSoundClip;

    [SerializeField] float rifleFireRate = 2f; 

    float nextFireTime = 0f;

    public int floatingObjectsLive = 0;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            FireShot();
            nextFireTime = Time.time + 1f / rifleFireRate;
        }

        print("FloatingObjects: " + floatingObjectsLive.ToString());
    }


    private void FireShot()
    {
        shotSound.PlayOneShot(shotSoundClip);

        Ray raycast = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(raycast, out RaycastHit hit))
        {
            if (hit.collider.GetComponent<GravityObject>())
            {
                GravityObject obj = hit.collider.GetComponent<GravityObject>();
                obj.ToggleGravity();
            }
        }
    }

    private void CheckForMaximumAmountOfObjects()
    {
        if (floatingObjectsLive >= 3)
        {
            GravityObject[] objs = FindObjectsOfType<GravityObject>();

        }
    }
}
