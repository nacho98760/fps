using UnityEngine;

public class ObjObservationScript : MonoBehaviour
{
    [SerializeField] LayerMask observableObjLayer;

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 25f, Color.red);
        HandleObjObservation();
    }

    void HandleObjObservation()
    {
        print(Physics.Raycast(transform.position, transform.forward, out RaycastHit hits, 30f, observableObjLayer));
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 30f, observableObjLayer))
        {
            ObservableObject obj = hit.collider.GetComponent<ObservableObject>();

            if (obj == null)
                return;

            //If obj.isCurrentlyObserved == false, then we know the player just started observing the object
            if (obj.isCurrentlyObserved == false)
            {
                obj.isCurrentlyObserved = true;

                if (obj.isItFirstTimeObservation)
                {
                    obj.isItFirstTimeObservation = false;
                    return;
                }
            }
        }
        else
        {
            ChangeObservedObjPosition();
        }
    }

    void ChangeObservedObjPosition()
    {
        foreach (ObservableObject obj in FindObjectsByType<ObservableObject>(FindObjectsSortMode.None))
        {
            if (obj.isCurrentlyObserved)
            {
                obj.isCurrentlyObserved = false;
                obj.transform.position = obj.objFirstStatePosition;
            }
        }
    }
}