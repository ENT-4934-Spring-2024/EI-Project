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
        public float directionChangeInterval = 10f; // Time interval for changing direction
        private float timer;
        //--------------------------------------------//


        private void Start()
        {
            myMimic = GetComponent<Mimic>();

            //my code
            // Initialize the timer
            timer = directionChangeInterval;

            // Start the mimic moving in a random direction initially
            ChangeDirection();

        }

        void Update()
        {
            //--------------- my code -------------------//
            // direction of movement

            // Reduce the timer
            timer -= Time.deltaTime;

            // If the timer has expired, change direction
            if (timer <= 0f)
            {
                ChangeDirection();
                timer = directionChangeInterval; // Reset the timer
            }

            // Update the position based on the current velocity
            transform.position += velocity * Time.deltaTime;

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

        void ChangeDirection()
        {
            // Generate a random direction
            Vector3 randomDirection = Random.insideUnitSphere.normalized;

            // Ensure the Y component is 0 to move only on the XZ plane
            randomDirection.y = 0f;

            // Set the new velocity based on the random direction and speed
            velocity = randomDirection * speed;
        }
    }

}