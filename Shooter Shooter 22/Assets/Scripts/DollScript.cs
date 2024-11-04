using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollScript : MonoBehaviour
{
    public GameObject Player;
    public Animator anim;
    [SerializeField]
    private int Chase = 20;
    private bool Chased;

    [Header("Combat")]
    [SerializeField]
    private int HP = 10;
    public GameObject Arm;


    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance <= Chase && !Chased  && distance >  0.5f)
        {
            StartCoroutine(SwapAnimations());

        }
        if (Chased)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 9 * Time.deltaTime);
            Vector3 direction = Player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle + 90, 0);
        }

        if (HP <= 0)
        {
            Instantiate(Arm, transform.position, Quaternion.identity);
            Destroy (gameObject);
        }
    }

    IEnumerator SwapAnimations()
    {
        anim.SetBool("SWAP", true);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("SWAP", false);
        anim.SetBool("SWAP2", true);
        yield return new WaitForSeconds(0.5f);
        Chased = true;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Syth"))
        {
            HP--;
        }
    }



}
