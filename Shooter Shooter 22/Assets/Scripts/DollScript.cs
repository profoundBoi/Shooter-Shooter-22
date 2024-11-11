using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DollScript : MonoBehaviour
{
    public GameObject Player;
    public Animator anim;
    [SerializeField]
    private int Chase = 30;
    [SerializeField]
    private int Shoot = 20;
    private bool Chased;

    [Header("Combat")]
    [SerializeField]
    private int HP = 10;
    public GameObject Arm;
    public GameObject Ammo;
    public Slider HPSlider;
    public GameObject HPBar;
    [SerializeField]
    private bool CanShoot = false;
    public Transform ShootPoint;
    private int StartCR;

    private void Start()
    {
        StartCoroutine(Shooter());
    }


    void Update()
    {
        if (gameObject.activeSelf && StartCR < 1)
        {
            StartCR++;
            Start();
        }



        HPSlider.value = HP;    
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance <= Chase && !Chased )
        {
            StartCoroutine(SwapAnimations());

        }
        if (Chased )
        {
            if (distance < Chase && distance > Shoot)
            {
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 6 * Time.deltaTime);
                Vector3 direction = Player.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, angle + 90, 0);

            }
            else if (distance < Chase && distance <= Shoot)
            {
                if (CanShoot)
                {
                    transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 6 * Time.deltaTime);
                    Vector3 direction = Player.transform.position - transform.position;
                    float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, angle + 90, 0);


                    GameObject Flame = Instantiate(Ammo, transform.position, Quaternion.identity);
                    Rigidbody rb = Flame.GetComponent<Rigidbody>();
                    rb.velocity = direction * 7;
                    Destroy(Flame, 2);
                }
                else {
                    transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 6 * Time.deltaTime);
                    Vector3 direction = Player.transform.position - transform.position;
                    float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, angle + 90, 0);
                }
            }
        }

        if (HP <= 0)
        {
            Instantiate(Arm, transform.position, Quaternion.identity);
            Destroy (gameObject);
            HPBar.SetActive(false);

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
        GameObject Syth = GameObject.FindGameObjectWithTag("Syth");
        MeshCollider SC = Syth.GetComponent<MeshCollider>();


        if (SC.isTrigger && other.gameObject.CompareTag("Syth"))
        {
            
                HP--;
            
        }

        else if (other.gameObject.CompareTag("Bullet"))
        {
            HP--;
        }
    }

  



    IEnumerator Shooter()
    {
       
        CanShoot = true;
        yield return new WaitForSeconds(0.1f);
        CanShoot = false;
        yield return new WaitForSeconds(3);
        StartCoroutine(Shooter());
    }
}
