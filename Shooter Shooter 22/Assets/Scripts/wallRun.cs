using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallRun : MonoBehaviour
{
    public float wallRunSpeed = 10f; // Speed during wall run
    public float wallRunDuration = 10f; // Duration of the wall run
   
   

    private CharacterController characterController; // Reference to the CharacterController component
  
    private FirstPersonControl fr; // Reference to the PlayerMovement script

    private void Start()
    {
        // Get and store the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();
        fr = GetComponent<FirstPersonControl>();
    }

    private void Update()
    {
     
    }

    void OnCollisionEnter(Collision other)
    {
        // Check if the player collides with an object tagged as "Wall"
        if (other.gameObject.CompareTag("Wall"))
        {
            fr.gravity = 0f; // Disable gravity during wall run
            fr.moveSpeed = wallRunSpeed; // Set wall run speed
            print("wow you are wall running");
        }
    }

    void OnCollisionExit(Collision other)
    {
        // Check if the player stops colliding with the object tagged as "Wall"
        if (other.gameObject.CompareTag("Wall"))
        {
            fr.gravity = -9.81f; // Reset gravity to normal value
            fr.moveSpeed = 5f; // Reset move speed to normal value
        }
    }

  

   

    
}
