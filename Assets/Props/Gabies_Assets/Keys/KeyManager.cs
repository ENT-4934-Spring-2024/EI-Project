using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyCollect : MonoBehaviour
{
    public Key Key1;
    public int keycCollected;

    public void Manager()
    {
        Key1.GrabKey();


        if(keycCollected > 6)
        {
            //end game
        }
        
    }
}