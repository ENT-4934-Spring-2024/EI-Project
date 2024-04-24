using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MimicSpace
{
    /// <summary>
    /// This is a very basic movement script, if you want to replace it
    /// Just don't forget to update the Mimic's velocity vector with a Vector3(x, 0, z)
    /// </summary>
    public class Movement : MonoBehaviour
    {
        [Header("Controls")]
        [Tooltip("Body Height from ground")]
        [Range(0.5f, 5f)]
        public float height = 0.8f;
        public float speed = 5f;
        Vector3 velocity = Vector3.zero;
        public float velocityLerpCoef = 4f;
        Mimic myMimic;

        //--------------- my code -------------------//
        public Transform player; // Reference to the player's transform
        public float followDistance = 10f; // Distance threshold for following the player
        public float teleportRadius = 20f; // Radius for teleporting when too far from the player
        
        //--------------------------------------------//


        private void Start()
        {
            myMimic = GetComponent<Mimic>();

            //my code
            // Find the player GameObject with the tag "Player"
            player = GameObject.FindGameObjectWithTag("Player").transform;

        }

        void Update()
        {
            //--------------- my code -------------------//


            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Calculate direction towards the player
            velocity = (player.position - transform.position).normalized;

            myMimic.velocity = velocity;

            RaycastHit hit;
            Vector3 destHeight = transform.position * height;
            if (Physics.Raycast(transform.position + Vector3.up * 5f, -Vector3.up, out hit))
            {
                destHeight = new Vector3(transform.position.x, hit.point.y + height, transform.position.z);
            }

            if (distanceToPlayer < followDistance)
            {
                // Move the mimic towards the player
                transform.position += velocity * speed * Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, destHeight, velocityLerpCoef * Time.deltaTime);
            }
            else
            {
                //teleport mimic to a random position within radius
                //followDistance MUST be less than teleportRadius !!!
                transform.position = player.position + Random.insideUnitSphere * teleportRadius;
            }
            
            

            //----------------------------------------//


            // Update the position based on the current velocity
            //transform.position += velocity * Time.deltaTime;

            //velocity = Vector3.Lerp(velocity, new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed, velocityLerpCoef * Time.deltaTime);

            /*Vector3 moveDirection = transform.forward;
            velocity = Vector3.Lerp(velocity, moveDirection.normalized * speed, velocityLerpCoef * Time.deltaTime);
            */



            // Assigning velocity to the mimic to assure great leg placement
            //myMimic.velocity = velocity;

           /*
            
            transform.position = transform.position + velocity * Time.deltaTime;
            RaycastHit hit;
            Vector3 destHeight = transform.position;
            if (Physics.Raycast(transform.position + Vector3.up * 5f, -Vector3.up, out hit))
            {
                destHeight = new Vector3(transform.position.x, hit.point.y + height, transform.position.z);
            }
            transform.position = Vector3.Lerp(transform.position, destHeight, velocityLerpCoef * Time.deltaTime);

            */
        }

        
    }

}