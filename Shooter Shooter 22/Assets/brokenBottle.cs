using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class brokenBottle : MonoBehaviour
{
    private MeshCollider Col;

    private void Start()
    {
        Col = GetComponent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Stair") || collision.gameObject.CompareTag("Door"))
        {
            Col.isTrigger = false;
        }
    }
}
