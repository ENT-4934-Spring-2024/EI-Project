using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogMovement : MonoBehaviour
{
    public Transform player; // Reference to the player's transform

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Set the fog object's position to match the player's position
            transform.position = player.position;
        }
        else
        {
            // Find the player GameObject with the tag "Player"
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }
}
