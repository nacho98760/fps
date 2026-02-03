using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GravityObject : MonoBehaviour
{
    private Rigidbody rb;
    public GravityActivationLogic gravityActivationScript;

    public bool isLifting = false;
    public bool isFrozen = false;
    Coroutine liftCoroutine;
    Coroutine collapseCoroutine;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ToggleGravity()
    {
        if (isLifting)
            return;

        if (isFrozen)
        {
            Release();
            return;
        }

        if (!IsObjectGrounded())
            return;

        liftCoroutine = StartCoroutine(LiftAndFreeze());
    }

    IEnumerator LiftAndFreeze()
    {
        isLifting = true;

        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        float timer = 0f;

        while (timer < 1f)
        {
            rb.AddForce(Vector3.up * 8f, ForceMode.Acceleration);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Freeze();
    }

    void Freeze()
    {
        gravityActivationScript.floatingObjectsLive++;
        isFrozen = true;
        isLifting = false;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.constraints = RigidbodyConstraints.FreezeAll;

        if (collapseCoroutine != null)
            StopCoroutine(collapseCoroutine);

        collapseCoroutine = StartCoroutine(StartCollapseCountdown());
    }

    public void Release()
    {
        if (collapseCoroutine != null)
        {
            StopCoroutine(collapseCoroutine);
            collapseCoroutine = null;
        }

        if (gravityActivationScript.floatingObjectsLive > 0)
        {
            gravityActivationScript.floatingObjectsLive--;
        }
        isFrozen = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
    }

    IEnumerator StartCollapseCountdown()
    {
        yield return new WaitForSeconds(5);
        if (isFrozen)
        {
            Release();
        }
    }

    bool IsObjectGrounded()
    {
        Collider objCollider = GetComponent<Collider>();

        Vector3 origin = objCollider.bounds.center;
        float distance = objCollider.bounds.extents.y + 0.05f;

        return Physics.Raycast(origin, Vector3.down, distance);
    }
}
