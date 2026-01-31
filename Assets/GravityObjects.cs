using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GravityObject : MonoBehaviour
{
    private Rigidbody rb;
    public GunLogic GunLogicScript;

    bool isLifting = false;
    bool isFrozen = false;
    Coroutine liftCoroutine;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ToggleGravity()
    {
        if (isLifting)
        {
            StopLiftAndFreeze();
            return;
        }

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

        GunLogicScript.floatingObjectsLive++;

        while (timer < 1f)
        {
            //rb.AddForce(Vector3.up * 8f, ForceMode.Acceleration);
            rb.MovePosition(rb.position + Vector3.up * 6f * Time.fixedDeltaTime);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Freeze();
    }

    void StopLiftAndFreeze()
    {
        if (liftCoroutine != null)
            StopCoroutine(liftCoroutine);

        isLifting = false;
        Freeze();
    }

    void Freeze()
    {
        isFrozen = true;
        isLifting = false;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void Release()
    {
        isFrozen = false;
        GunLogicScript.floatingObjectsLive--;
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
    }

    bool IsObjectGrounded()
    {
        Collider objCollider = GetComponent<Collider>();

        Vector3 origin = objCollider.bounds.center;
        float distance = objCollider.bounds.extents.y + 0.05f;

        return Physics.Raycast(origin, Vector3.down, distance);
    }
}
