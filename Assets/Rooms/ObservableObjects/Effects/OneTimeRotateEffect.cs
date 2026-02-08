using UnityEngine;

public class OneTimeRotateEffect : MonoBehaviour
{
    ObservableObject obj;
    bool wasObjSeenOnce;
    bool triggered;

    private void Awake()
    {
        obj = GetComponent<ObservableObject>();
    }

    private void Start()
    {
        triggered = false;
        wasObjSeenOnce = false;
    }

    private void Update()
    {
        if (triggered) return;

        if (obj.isObserved)
        {
            wasObjSeenOnce = true;
        }
        else
        {
            if (wasObjSeenOnce)
            {
                triggered = true;
                transform.localRotation = Quaternion.Euler(0f, 200f, 0f);
            }
        }
    }
}
