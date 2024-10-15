using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeachScript : MonoBehaviour
{
    public TextMeshProUGUI speachText;
    public GameObject SpeachBox;

    [Header("Scarey LongLegs")]
    public GameObject ScaryLongLegs;
    public Transform ScaryLegsPosition;
    public Transform ScaryLegsposition2;

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
    }

    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(13);
        SpeachBox.SetActive(true);
        Destroy(Eyes);
        foreach (GameObject t in inGameUI)
        {
            t.SetActive(true);
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
        if (hit.collider.gameObject.CompareTag("MirrorScare"))
        {
            Destroy(hit.gameObject);
            GameObject Monster = Instantiate(ScaryLongLegs, ScaryLegsPosition.position, Quaternion.identity);
            Destroy(Monster, 1.5f);

            

        }

        if (hit.collider.gameObject.CompareTag("DoorScare"))
        {
            Destroy(hit.gameObject);
            GameObject Monster = Instantiate(ScaryLongLegs, ScaryLegsposition2.position, Quaternion.identity);
            Monster.transform.rotation = ScaryLegsposition2.rotation;
            StartCoroutine(DoorScare());
            Destroy(Monster, 1.5f);
            GetGun = true;
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
    IEnumerator DoorScare()
    {
        yield return new WaitForSeconds(0);
        SpeachBox.SetActive(true);
        speachText.text = "What was that";
        yield return new WaitForSeconds(2);
        speachText.text = "Let me grab Dads weapon from the Safe";
        yield return new WaitForSeconds(3);
        speachText.text = "I Need a weapon if i want to get out of here";
        yield return new WaitForSeconds(3);
        speachText.text = "Let Me use Dads weapons from the safe";
        yield return new WaitForSeconds(3);
        speachText.text = "Luckily he keeps important things in the Safe";
        yield return new WaitForSeconds(3);
        speachText.text = "";
        SpeachBox.SetActive(false);



    }

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
        if (scare)
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
