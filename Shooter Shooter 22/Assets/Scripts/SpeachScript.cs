using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeachScript : MonoBehaviour
{
    public TextMeshProUGUI speachText;
    public GameObject SpeachBox;

    [Header("Rabbit Activate")]
    public GameObject RabbitActive;

    [Header ("UI")]
    public List<GameObject> inGameUI;
    public GameObject Eyes;

    public bool GetGun;
    // Start is called before the first frame update
    void Start()
    {
        SpeachBox.SetActive(false);
        Eyes.SetActive(true);
        foreach (GameObject t in inGameUI)
        {
            t.SetActive(false);
        }
        StartCoroutine(WakeUp());

        RabbitActive.SetActive(false);
    }

    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(13);
        SpeachBox.SetActive(true);
        Destroy(Eyes);
        foreach (GameObject t in inGameUI)
        {
            t.SetActive(true);
            Debug.Log(",,,,,,,,,,,,,,,,,,,,,,,,,,,,,");
        }
        speachText.text = "What... What Happened";
        yield return new WaitForSeconds(4);
        speachText.text = "Where did everyone go?";
        yield return new WaitForSeconds (4);
        speachText.text = "Who left a letter on the night stand?";
        yield return new WaitForSeconds(4);
        SpeachBox.SetActive(false) ;
        speachText.text = "";



    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.CompareTag("RabbitActive"))
        {
            Destroy(hit.gameObject);
            RabbitActive.SetActive(true) ;
        }

        
        if (hit.collider.gameObject.CompareTag("Scare2"))
        {
            Destroy(hit.gameObject);
            scare = true;
            
        }

        if (hit.collider.gameObject.CompareTag("Rabbit"))
        {
            Destroy (hit.gameObject);
        }

        if (hit.collider.gameObject.CompareTag("GameRoom"))
        {
            Destroy(hit.gameObject);
            SpeachBox.SetActive(true);
            StartCoroutine(GameRoomTrigger());

        }

        if (hit.collider.gameObject.CompareTag("StorageRoom"))
        {
            Destroy(hit.gameObject);
            SpeachBox.SetActive(true);
            StartCoroutine(StorageRoomTrigger());
        }


    }
    private bool scare;
    public GameObject Rabbit;
    public GameObject Player;
    

    IEnumerator StorageRoomTrigger()
    {
        yield return new WaitForSeconds(0);
        SpeachBox.SetActive(true);
        speachText.text = "What happened in here";
        yield return new WaitForSeconds(2);
        speachText.text = "All these Boxes scatted around";
        yield return new WaitForSeconds(3);
        speachText.text = "Dads going to be mad at me";
        yield return new WaitForSeconds(3);
        speachText.text = "Whats that on the table?";
        yield return new WaitForSeconds(3);
        speachText.text = "Another one of those weird letters!";
        yield return new WaitForSeconds(3);
        speachText.text = "";
        SpeachBox.SetActive(false);



    }

    IEnumerator GameRoomTrigger()
    {
        SpeachBox.SetActive(true);
        speachText.text = "Oh no! Why is the storage room open!";
        yield return new WaitForSeconds(2);
        speachText.text = "I hope nothing bad happened!";
        yield return new WaitForSeconds(3);
        speachText.text = "";
        SpeachBox.SetActive(false);



    }
    [Header("Scare Audio")]
    public AudioClip ScareSound;
    [SerializeField]
    AudioSource SFXSRCE;
    private void Update()
    {
        if (scare && Rabbit != null)
        {
            Rabbit.transform.position = Vector3.MoveTowards(Rabbit.transform.position, Player.transform.position, 100 * Time.deltaTime);
            Rabbit.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
            Destroy(Rabbit, 0.3f);
            StartCoroutine(ScareSounds());
        }
    }

    IEnumerator ScareSounds()
    {
        yield return new WaitForSeconds(0);
        SFXSRCE.PlayOneShot(ScareSound);
        yield return new WaitForSeconds(1);
        SFXSRCE.Stop();

    }

}
