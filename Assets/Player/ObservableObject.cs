using UnityEngine;

public class ObservableObject : MonoBehaviour
{
    public Vector3 objOriginalPosition;
    public Vector3 objFirstStatePosition;
    public bool isItFirstTimeObservation;
    public bool isCurrentlyObserved;

    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask observableObjLayer;
    Renderer objRenderer;

    void Awake()
    {
        objRenderer = GetComponent<Renderer>();
    }

    void Start()
    {
        isItFirstTimeObservation = true;
        objOriginalPosition = transform.position;
        objFirstStatePosition = objOriginalPosition + new Vector3(0f, 0f, 6f);
    }

    void Update()
    {
        print(objRenderer.isVisible);
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 25f, Color.red);
        if (objRenderer.isVisible)
        {
            if (HasLineOfSight())
            {
                if (isItFirstTimeObservation)
                {
                    isItFirstTimeObservation = false;
                    return;
                }
            }
            else
            {
                if (isItFirstTimeObservation == false)
                {
                    MoveObject();
                }
            }
        }
        else
        {
            if (isItFirstTimeObservation == false)
            {
                MoveObject();
            }
        }
    }

    void MoveObject()
    {
        transform.position = objFirstStatePosition;
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
