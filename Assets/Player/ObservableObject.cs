using UnityEngine;

public class ObservableObject : MonoBehaviour
{
    Vector3 objSecondStatePosition;
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
        objSecondStatePosition = transform.position + new Vector3(0f, 0f, 2f);
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
            transform.rotation = Quaternion.Euler(0f, 210f, 0f);
        }
        else if (state == 2)
        {
            transform.position = objSecondStatePosition;
        }
        else
        {
            return;
        }
    }

    bool HasLineOfSight()
    {
        Vector3 direction = (transform.position - playerCamera.transform.position).normalized;

        if (Physics.Raycast(playerCamera.transform.position, direction, out RaycastHit hit, 30f, observableObjLayer))
        {
            return hit.transform == transform;
        }

        return false;
    }
}
