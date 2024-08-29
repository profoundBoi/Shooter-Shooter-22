using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    public GameObject leftDoor;
    public GameObject rightDoor;



    private Vector3 rightposition = new Vector3(-5, 24, 263);
   private Vector3 leftposition = new Vector3(-5    , 24, 240);

    private bool OpenDoors = false;
    private bool CloseDoors = false;


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
        if (OpenDoors == true && leftDoor.transform.position.z > 240 )
        {
            leftDoor.transform.position = Vector3.MoveTowards(leftDoor.transform.position, leftposition, 3 * Time.deltaTime);
            rightDoor.transform.position = Vector3.MoveTowards(rightDoor.transform.position, rightposition, 3 * Time.deltaTime);
        }
        else if (OpenDoors == false && leftDoor.transform.position.z < 248)
        {
            leftDoor.transform.position = Vector3.MoveTowards(leftDoor.transform.position, new Vector3(-5, 24, 248), 3 * Time.deltaTime);
            rightDoor.transform.position = Vector3.MoveTowards(rightDoor.transform.position, new Vector3(-5, 24, 256), 3 * Time.deltaTime);
        }
       
       

        
        Debug.Log(rightDoor.transform.position);
    }
}
