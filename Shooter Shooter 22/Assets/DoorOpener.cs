using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    public GameObject leftDoor;
    public GameObject rightDoor;


    public bool OpenDoors = false;
    public bool CloseDoors = false;


    public Transform leftpos;
    public Transform rightpos;
    public Transform leftMiddlepost;
    public Transform rightMiddlepost;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenDoors = true;
            CloseDoors = false;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenDoors = false ;
            CloseDoors = true;
        }
    }

    void Update()
    {
        if (OpenDoors == true )
        {
            leftDoor.transform.position = Vector3.MoveTowards(leftDoor.transform.position, leftpos.position, 3 * Time.deltaTime);
            rightDoor.transform.position = Vector3.MoveTowards(rightDoor.transform.position, rightpos.position, 3 * Time.deltaTime);
        }
        else if (CloseDoors == true)
        {
            leftDoor.transform.position = Vector3.MoveTowards(leftDoor.transform.position, leftMiddlepost.position, 3 * Time.deltaTime);
            rightDoor.transform.position = Vector3.MoveTowards(rightDoor.transform.position, rightMiddlepost.position, 3 * Time.deltaTime);
        }
       
       

        
        Debug.Log(rightDoor.transform.position);
    }
}
