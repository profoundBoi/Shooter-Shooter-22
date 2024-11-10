using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance < 10)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 15 * Time.deltaTime);
        }
        
    }
}
