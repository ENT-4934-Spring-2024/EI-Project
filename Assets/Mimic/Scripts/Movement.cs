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
        public string playerTag = "Player";
        private Vector3 initialPlayerPosition; // Initial position of the player
        private Transform playerTransform;
        private float followTimer = 0f; // Timer to track the follow duration
        public float followDuration = 5f; // Duration for which the mimic follows the player
        //--------------------------------------------//


        private void Start()
        {
            myMimic = GetComponent<Mimic>();

            //my code
            initialPlayerPosition = playerTransform.position;
        }

        void Update()
        {
            //--------------- my code -------------------//
            // direction of movement

            // Update the follow timer
            followTimer += Time.deltaTime;

            if (playerTransform != null)
            {
                // Check if the follow duration has not elapsed
                if (followTimer < followDuration)
                {
                    // Calculate direction to the player's initial position
                    Vector3 direction = playerTransform.position - transform.position; direction.y = 0f;
                    // Ignore vertical movement

                    // Smoothly move the mimic towards the player's initial position
                    transform.Translate(direction.normalized * speed * Time.deltaTime);

                    followTimer += Time.deltaTime;
                }
            }

            //velocity = Vector3.Lerp(velocity, new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed, velocityLerpCoef * Time.deltaTime);

            /*Vector3 moveDirection = transform.forward;
            velocity = Vector3.Lerp(velocity, moveDirection.normalized * speed, velocityLerpCoef * Time.deltaTime);
            */



            // Assigning velocity to the mimic to assure great leg placement
            myMimic.velocity = velocity;

            
            transform.position = transform.position + velocity * Time.deltaTime;
            RaycastHit hit;
            Vector3 destHeight = transform.position;
            if (Physics.Raycast(transform.position + Vector3.up * 5f, -Vector3.up, out hit))
            {
                destHeight = new Vector3(transform.position.x, hit.point.y + height, transform.position.z);
            }
            transform.position = Vector3.Lerp(transform.position, destHeight, velocityLerpCoef * Time.deltaTime);
        }
    }

}