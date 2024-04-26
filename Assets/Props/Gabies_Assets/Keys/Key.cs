using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Key : MonoBehaviour
{
    public KeyCollect Manager;

    public void GrabKey()
    {
        Manager.keycCollected++;
        Disable();
        
    }

    public void Disable()
    {
        // Deactivate the key GameObject
        gameObject.SetActive(false);
    }
}