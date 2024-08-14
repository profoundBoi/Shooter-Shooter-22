using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallRun : MonoBehaviour
{
    [Header("wallRunSettings")]
    [Space(5)]
    public FirstPersonControl fr;
    public float wallRunSpeed;
    public float wallRunTime;
    private GameObject wall;
    private GameObject player;

    private Rigidbody rb;
    private bool isWallRunning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = this.gameObject;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player has collided with the wall
        if (collision.gameObject.CompareTag("wall")) // Using tags to identify the wall
        {
            wall = collision.gameObject; // Store the wall reference
            StartWallRun(); // Start wall run logic
        }
    }

    void StartWallRun()
    {
        if (!isWallRunning)
        {
            isWallRunning = true;

            // Set the player's velocity to move along the wall
            Vector3 wallRunDirection = Vector3.Cross(wall.transform.forward, Vector3.up).normalized; // Determine wall run direction
            rb.velocity = wallRunDirection * wallRunSpeed;

            // Adjust the player's orientation to match the wall
            Vector3 wallNormal = wall.transform.forward;
            Quaternion targetRotation = Quaternion.LookRotation(wallNormal);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, Time.deltaTime * 10f);

           
        }
    }

   
}
