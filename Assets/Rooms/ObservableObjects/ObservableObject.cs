using System;
using UnityEngine;

public class ObservableObject : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public DecayProfile decayProfile;

    public float Stability { get; private set; }

    public bool usesStability;

    public bool isObserved;
    public bool isFocused;
    float decayTimer;

    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask observableObjLayer;
    Renderer objRenderer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        objRenderer = GetComponent<Renderer>();
    }
    private void Start()
    {
        Stability = decayProfile.maxStability;
    }

    void Update()
    {
        UpdateObservation();

        if (transform.root.name == player.GetComponent<PlayerMovement>().playerCurrentRoom && usesStability)
        {
            print(Stability);
            UpdateStability();
        }
    }

    void UpdateObservation()
    {

        if (objRenderer.isVisible == false)
        {
            isObserved = false;
            isFocused = false;
            return;
        }

        if (IsWithinRange())
        {
            isObserved = true;

            if (IsBeingFocused())
            {
                isFocused = true;
            }
            else
            {
                isFocused = false;
            }
        }
        else
        {
            isObserved = false;
            isFocused = false;
        }
    }

    bool IsWithinRange()
    {
        BoxCollider chairCollider = GetComponent<BoxCollider>();
        Vector3 toObject = (chairCollider.bounds.center - playerCamera.transform.position).normalized;
        float dot = Vector3.Dot(playerCamera.transform.forward, toObject);

        if (dot < 0.62f) return false;

        Vector3 direction = (transform.position - playerCamera.transform.position).normalized;

        if (Physics.Raycast(playerCamera.transform.position, direction, out RaycastHit hit, 25f))
        {
            return hit.transform == transform;
        }

        return false;
    }

    bool IsBeingFocused()
    {
        BoxCollider chairCollider = GetComponent<BoxCollider>();
        Vector3 toObject = (chairCollider.bounds.center - playerCamera.transform.position).normalized;
        float dot = Vector3.Dot(playerCamera.transform.forward, toObject);

        if (dot < 0.9f) return false;

        Vector3 direction = (transform.position - playerCamera.transform.position).normalized;

        if (Physics.Raycast(playerCamera.transform.position, direction, out RaycastHit hit, 20f))
        {
            return hit.transform == transform;
        }

        return false;
    }

    void UpdateStability()
    {
        if (isFocused)
        {
            decayTimer = 0f;
            Stability += decayProfile.recoveryRate * Time.deltaTime;
        }
        else
        {
            decayTimer += Time.deltaTime;
            if (decayTimer >= decayProfile.decayDelay)
                Stability -= decayProfile.decayRate * Time.deltaTime;
        }

        Stability = Mathf.Clamp(
            Stability,
            decayProfile.minStability,
            decayProfile.maxStability
        );
    }

}
