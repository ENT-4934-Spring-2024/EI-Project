using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketObjectSpinner : MonoBehaviour
{
    // Speed of rotation
    public float rotationSpeed = 30f;

    // Reference to the XR Socket Interactor
    private XRSocketInteractor socketInteractor;

    // Reference to the object currently connected to the socket
    private GameObject connectedObject;

    void Awake()
    {
        // Get the XR Socket Interactor component attached to this GameObject
        socketInteractor = GetComponent<XRSocketInteractor>();

        // Check if XRSocketInteractor component is found
        if (socketInteractor != null)
        {
            // Subscribe to the socket events
            socketInteractor.selectEntered.AddListener(HandleObjectAttached);
            socketInteractor.selectExited.AddListener(HandleObjectDetached);
        }
        else
        {
            Debug.LogWarning("XRSocketInteractor component not found on GameObject: " + gameObject.name);
        }
    }

    void Update()
    {
        // If an object is connected to the socket, rotate it
        if (connectedObject != null)
        {
            connectedObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    // Event handler for when an object is attached to the socket
    void HandleObjectAttached(SelectEnterEventArgs args)
    {
        connectedObject = args.interactable.gameObject;
    }

    // Event handler for when an object is detached from the socket
    void HandleObjectDetached(SelectExitEventArgs args)
    {
        connectedObject = null;
    }
}
