using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    public GameObject Player;
    public Animator Walk;

    [SerializeField]
    private int HPs = 4;
    [SerializeField]
    public GameObject eyeCollectable;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        
        float distance = Vector3.Distance (transform.position, Player.transform.position);

        if (distance <= 20 && distance > 0.5f)
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

    public GameObject Blood;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet") )
        {
            HPs--;

            if (HPs == 0)
            {
                GameObject Bloodtrail = Instantiate(Blood, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(Bloodtrail, 0.5f);
                int Eye = Random.Range(0, 10);
                if (Eye == 7)
                {
                    Instantiate(eyeCollectable, transform.position, Quaternion.identity);
                }

                Debug.Log(Eye);
            }
            
            
        }
        else if (other.gameObject.CompareTag("Nothing") || other.gameObject.CompareTag("Syth"))
        {
            GameObject Syth = GameObject.FindGameObjectWithTag("Syth");
            MeshCollider SC = Syth.GetComponent<MeshCollider>();

           
            if (SC.isTrigger)
            {
                HPs -= 4;
                if (HPs <= 0)
                {
                    GameObject Bloodtrail = Instantiate(Blood, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    Destroy(Bloodtrail, 0.5f);
                    int Eye = Random.Range(0, 10);
                    if (Eye == 7)
                    {
                        Instantiate(eyeCollectable, transform.position, Quaternion.identity);
                    }
                }
            }
            
            else { return; }


        }
    }
}
