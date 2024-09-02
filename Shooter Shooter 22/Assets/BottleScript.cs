using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BottleScript : MonoBehaviour
{
    public GameObject Bottle;
    public GameObject brokenBottle;

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Stair") || collision.gameObject.CompareTag("Door"))
        {
            if (Bottle.tag == "Bottle")
            {
                brokenBottle.SetActive(true);
                Bottle.SetActive(false);
                MeshCollider col = brokenBottle.GetComponent<MeshCollider>();
                col.enabled = true;
            }
        }

    }
}
