using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScript : MonoBehaviour
{
    public Transform Flash;
    public int pickUpRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Flash.position, Flash.forward);
        RaycastHit hit;
        Debug.DrawRay(Flash.position, Flash.forward * pickUpRange, Color.red, pickUpRange);
        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            if (hit.collider.CompareTag("Rabbit"))
            {
                Destroy(hit.collider.gameObject, 3);
            }
        }
    }
}
