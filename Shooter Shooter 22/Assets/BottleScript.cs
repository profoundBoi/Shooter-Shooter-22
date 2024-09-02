using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BottleScript : MonoBehaviour
{
  
    public GameObject brokenBottle;

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Syth") || collision.gameObject.CompareTag("Knife"))
        {
            
              Instantiate (brokenBottle, transform.position, transform.rotation );
            Destroy(gameObject);
               
            
        }

    }
}
