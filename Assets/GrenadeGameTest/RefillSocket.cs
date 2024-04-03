using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AutoCreateObjectOnGrab : MonoBehaviour
{
    public GameObject objectToCreate; // The object to instantiate


    // This method will be called whenever an interactor selects this socket
    public void OnSelectEntered(XRBaseInteractor interactor)
    {
        // Check if the object to create is assigned
        if (objectToCreate != null)
        {
            // Instantiate the object at the socket's position and rotation
            GameObject createdObject = Instantiate(objectToCreate, transform.position, transform.rotation);
             

            // You may want to parent the created object to the interactor's transform
            createdObject.transform.parent = interactor.transform;
        }
        else
        {
            Debug.LogWarning("Object to create is not assigned in the inspector.");
        }
    }

}
