using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargerManager : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            // this is going to destroy the bullet as soon as it collides with the Target game object. Done to prevent the bullet from bouncing.
            Destroy(other.gameObject);

            // this is going to destroy the Target that the player shoots at.
            Destroy(gameObject); 

            // Check is the Bullet has collided with the target basically.
            Debug.Log("Target Destroyed");
        }
    }
}
