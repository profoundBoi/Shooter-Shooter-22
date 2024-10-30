using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class BatScript : MonoBehaviour
{
    public Transform[] Spots;
    private Transform nextSpot;


    public GameObject Fang;

    [SerializeField]
    private int HP = 2;
    public GameObject Blood;

    private void Start()
    {
        nextSpot = Spots[Random.Range(0, Spots.Length)];
    }
    private void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, nextSpot.position, 9 * Time.deltaTime);
        Vector3 direction = nextSpot.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);

        if (Vector3.Distance(transform.position, nextSpot.position) < 0.1f)
        {
            // Pick a new random spot
            nextSpot = Spots[Random.Range(0, Spots.Length)];
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            HP--;

            if (HP == 0)
            {
                GameObject Bloodtrail = Instantiate(Blood, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(Bloodtrail, 0.5f);
                int Eye = Random.Range(1, 2);
                if (Eye == 1)
                {
                    Instantiate(Fang, transform.position, Quaternion.identity);
                }

                Debug.Log(Eye);
            }

        }
    }
}
