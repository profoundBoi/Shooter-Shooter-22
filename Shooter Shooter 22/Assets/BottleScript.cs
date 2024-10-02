using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BottleScript : MonoBehaviour
{
  
    public GameObject brokenBottle;



    private void OnTriggerEnter(Collider collision)
    {
        
    
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Syth") || collision.gameObject.CompareTag("Knife"))
        {
            
            GameObject BB =   Instantiate (brokenBottle, transform.position, transform.rotation );
            Destroy(gameObject);
               
            
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("Door"))
        {
            GameObject BB = Instantiate(brokenBottle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
