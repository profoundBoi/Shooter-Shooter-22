using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    public GameObject Player;
    public Animator Walk;

    private int HP;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        
        float distance = Vector3.Distance (transform.position, Player.transform.position);

        Debug.Log(distance);
        if (distance <= 20)
        {
            Walk.SetBool("Walk", true);
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 9 * Time.deltaTime);
            Vector3 direction = Player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle + 90, 0);

               
        }
        else if (distance > 25) { 
            Walk.SetBool("Walk", false);
            transform.rotation = transform.rotation;
        
        }

        if (ShotS ==2)
        {
            Destroy(gameObject);
        }
    }
    public int ShotS;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            ShotS++;
            Destroy(other.gameObject);
        }
    }
}
