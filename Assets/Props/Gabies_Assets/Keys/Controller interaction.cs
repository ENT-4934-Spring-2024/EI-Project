using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    public LayerMask keyLayerMask; // Layer mask for the keys
    private GameObject grabbedObject; // Currently grabbed object

    void Update()
    {
        // Detect controller trigger input
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Change to appropriate VR input
        {
            GrabKey();
        }
    }

    void GrabKey()
    {
        // Perform a raycast from the controller to detect keys
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, keyLayerMask))
        {
            // Check if the hit object has a Key script attached
            Key key = hit.collider.GetComponent<Key>();
            if (key != null)
            {
                key.Collect(); // Call the Collect method of the Key script
            }
        }
    }
}

public class Key : MonoBehaviour
{
    public void Collect()
    {
        // Deactivate the key GameObject
        gameObject.SetActive(false);

        // Optionally, update the player's inventory or perform other actions
    }
}
