using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    public GameObject Player;
    public Animator Walk;

    private void Update()
    {
        
        float distance = Vector3.Distance (transform.position, Player.transform.position);

        Debug.Log(distance);
        if (distance <= 20)
        {
            Walk.SetBool("Walk", true);
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 6 * Time.deltaTime);
            Vector3 direction = Player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle + 90, 0);

               
        }
        else if (distance > 20) { 
            Walk.SetBool("Walk", false);
            transform.rotation = transform.rotation;
        
        }


    }
}