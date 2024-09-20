using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gun"))
        {
            return;

        }
        else if (other.gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
