using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class NonKinematicOnGrab : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private Vector3 startPos;
    private bool hasBeenGrabbed = false;

    void Start()
    {
        // Get the XRGrabInteractable component attached to this object
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Get the Rigidbody component attached to this object
        rb = GetComponent<Rigidbody>();

        startPos = grabInteractable.transform.position;

        // Subscribe to the grab events
        grabInteractable.onSelectEntered.AddListener(OnGrab);
    }

    void Update()
    {
        // Check if the object is not grabbed
        if (!grabInteractable.isSelected && !hasBeenGrabbed)
        {
            grabInteractable.transform.position = startPos;
        }
    }

    void OnGrab(XRBaseInteractor interactor)
    {
        hasBeenGrabbed = true;
        // Object is grabbed, enable physics interactions
        //rb.isKinematic = false;
    }
}
