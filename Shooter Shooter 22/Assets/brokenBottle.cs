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

    private void OnTriggerEnter(Collider other)
    {
        if (other != null) 
        {
            Col.isTrigger = false;
        }
    }

}
