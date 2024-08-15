using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
         
        if (collision.gameObject.CompareTag("Gun"))
        {
            return;

        } else if (collision.gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
