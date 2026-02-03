using System.Collections;
using UnityEngine;

public class GravityActivationLogic : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;

    GravityObject[] gravityObjects;
    bool isPlayerActivationOnCooldown = false;

    float nextFireTime = 0f;
    float platformActivationCooldown = 0.5f;

    public int floatingObjectsLive = 0;

    private void Start()
    {
        gravityObjects = FindObjectsByType<GravityObject>(FindObjectsSortMode.None);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            ActivateGravityOnObj();
            nextFireTime = Time.time + platformActivationCooldown;
        }

        CheckForMaxObjects();
    }


    private void ActivateGravityOnObj()
    {
        Ray raycast = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(raycast, out RaycastHit hit))
        {
            if (hit.collider.GetComponent<GravityObject>() && isPlayerActivationOnCooldown == false)
            {
                isPlayerActivationOnCooldown = true;
                GravityObject obj = hit.collider.GetComponent<GravityObject>();
                obj.ToggleGravity();
                StartCoroutine(ManagePlayerActivationCooldown());
            }
        }
    }

    private IEnumerator ManagePlayerActivationCooldown()
    {
        yield return new WaitForSeconds(1f);
        isPlayerActivationOnCooldown = false;
    }

    private void CheckForMaxObjects()
    {
        if (floatingObjectsLive > 3)
        {
            foreach (GravityObject obj in gravityObjects)
            {
                if (obj.isFrozen)
                {
                    obj.Release();
                }
            }
        }
    }
}
