using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightFlickeringEffect : MonoBehaviour
{
    RoomScript objRoomScript;
    ObservableObject obj;
    bool flickeringAlreadyTriggered;

    private void Awake()
    {
        obj = GetComponent<ObservableObject>();
        objRoomScript = transform.parent.GetComponent<RoomScript>();
    }

    private void Update()
    {
        ApplyFlickeringEffect();
    }

    public void ApplyFlickeringEffect()
    {
        foreach (GameObject spotlight in objRoomScript.roofSpotlightsInRoom)
        {
            StartCoroutine(FlickerCoroutine(spotlight));
        }
    }

    IEnumerator FlickerCoroutine(GameObject spotlight)
    {
        float baseIntensity = 8f;

        while (obj.Stability <= 0.7f) 
        {
            spotlight.GetComponent<Light>().intensity = UnityEngine.Random.Range(0.5f, baseIntensity);

            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.2f));
        }

        spotlight.GetComponent<Light>().intensity = baseIntensity;
    }
}
