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

        
    }
    public int ShotS;

    public GameObject Blood;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet") )
        {
            GameObject Bloodtrail = Instantiate(Blood, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
            Destroy(Bloodtrail, 0.5f);
            
        }
        else if (other.gameObject.CompareTag("Nothing") || other.gameObject.CompareTag("Syth"))
        {
            GameObject Syth = GameObject.FindGameObjectWithTag("Syth");
            MeshCollider SC = Syth.GetComponent<MeshCollider>();

            GameObject Knife = GameObject.FindGameObjectWithTag("Knife");
            MeshCollider KC = Syth.GetComponent<MeshCollider>();
            if (SC.isTrigger)
            {
                GameObject Bloodtrails = Instantiate(Blood, transform.position, Quaternion.identity);
                Destroy(Bloodtrails, 0.5f);
                Destroy(gameObject);
            }
            else if (KC.isTrigger)
            {
                GameObject Bloodtrail = Instantiate(Blood, transform.position, Quaternion.identity);
                Destroy(Bloodtrail, 0.5f);
                Destroy(gameObject);
            }
            else { return; }


        }
    }
}
