using UnityEngine;

public class ObservableObject : MonoBehaviour
{
    int observationState;
    bool alreadyChangedStateInThisObservation;

    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask observableObjLayer;
    Renderer objRenderer;

    void Awake()
    {
        objRenderer = GetComponent<Renderer>();
    }
    private void Start()
    {
        alreadyChangedStateInThisObservation = false;
    }

    void Update()
    {
        if (objRenderer.isVisible)
        {
            if (HasLineOfSight())
            {
                if (alreadyChangedStateInThisObservation == false)
                {
                    alreadyChangedStateInThisObservation = true;
                    observationState++;
                    return;
                }
            }
            else
            {
                MoveObject(observationState);
            }
        }
        else
        {
            MoveObject(observationState);
        }
    }

    void MoveObject(int state)
    {
        alreadyChangedStateInThisObservation = false;
        if (state == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 200f, 0f);
        }
        else
        {
            return;
        }
    }

    bool HasLineOfSight()
    {
        Vector3 direction = (transform.position - playerCamera.transform.position).normalized;

        if (Physics.Raycast(playerCamera.transform.position, direction, out RaycastHit hit, 30f))
        {
            return hit.transform == transform;
        }

        return false;
    }
}
