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

    [Header("Text Stuff")]
    [SerializeField]private List<string> wakeUpText, GameRoom, StorageRoom, Garage, DollFight;
    public GameObject wakeUpTextB, GameRoomB, StorageRoomB, GarageB, DollFightB;

    private string textToShow;

    public bool canMove, canLook;
    

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

        //Wake Up Text

        wakeUpText.Add("What... What Happened");
        wakeUpText.Add("Where did everyone go?");
        wakeUpText.Add("Who left a letter on the night stand?");

        //GameRoom Text

        GameRoom.Add("Oh no! Why is the storage room open!");
        GameRoom.Add("I hope nothing bad happened!");

        // Storage Room
        StorageRoom.Add("What happened in here");
        StorageRoom.Add("All these Boxes scatted around");
        StorageRoom.Add("Dad is going to Kill me");
        StorageRoom.Add("Whats that on the table?");
        StorageRoom.Add("Another one of those weird letters!");

        //Garage Text

        Garage.Add("Seems like Ill have to make the key myself");
        Garage.Add("I hope the key maker still works");
        Garage.Add("Good thing Dad Leaves the manual next to it");

        //Doll Fight
        DollFight.Add("I have to Get that Dolls Arm");
        DollFight.Add("I need to use the syth to get it");




    }

    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(13);
        SpeachBox.SetActive(true);
        wakeUpTextB.SetActive(true);
        textToShow = wakeUpText[0];
        Cursor.visible = true;
        canMove = false;
        canLook = false;

        foreach (GameObject t in inGameUI)
        {
            t.SetActive(true);
        }



    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        #region Jump Scare

        if (hit.collider.gameObject.CompareTag("Scare2"))
        {
            Destroy(hit.collider);
            scare = true;
        }

            if (hit.collider.gameObject.CompareTag("RabbitActive"))
        {
            Destroy(hit.gameObject);
            RabbitActive.SetActive(true) ;
        }

        if (hit.collider.gameObject.CompareTag("Rabbit"))
        {
            Destroy (hit.gameObject);
        }
        #endregion

        #region Collisions for Speaches
        if (hit.collider.gameObject.CompareTag("GameRoom"))
        {
            Destroy(hit.gameObject);
            SpeachBox.SetActive(true);
            GameRoomB.SetActive(true);
            textToShow = GameRoom[0];
            Cursor.visible = true;
            canMove = false;
            canLook = false;


        }

        if (hit.collider.gameObject.CompareTag("StorageRoom"))
        {
            Destroy(hit.gameObject);
            SpeachBox.SetActive(true);
            StorageRoomB.SetActive(true);
            textToShow = StorageRoom[0];
            Cursor.visible = true;
            canMove = false;
            canLook = false;


        }

        if (hit.collider.gameObject.CompareTag("Garage"))
        {
            Destroy(hit.gameObject);
            SpeachBox.SetActive(true);
            GarageB.SetActive(true);
            textToShow = Garage[0];
            Cursor.visible = true;
            canMove = false;
            canLook = false;

        }

        if (hit.collider.gameObject.CompareTag("DollFight"))
        {
            Destroy(hit.gameObject);
            SpeachBox.SetActive(true);
            DollFightB.SetActive(true);
            textToShow = DollFight[0];
            Cursor.visible = true;
            canMove = false;
            canLook = false;


        }
        #endregion


    }

    public void GameroomNext()
    {
        if (GameRoom.Count > 0)
        { 
            GameRoom.RemoveAt(0);
            textToShow = GameRoom[0];

        }
        else if (GameRoom.Count <= 0)
        {
            SpeachBox.SetActive(false);
            Cursor.visible = false;
            canMove = true;
            canLook = true;
        }
    }

    public void StorageroomNext()
    {
        if (StorageRoom.Count > 0)
        { 
            StorageRoom.RemoveAt(0);
            textToShow = StorageRoom[0];

        }
        else if (StorageRoom.Count <= 0)
        { 
            SpeachBox.SetActive(false);
            Cursor.visible = false;
            canMove = true;
            canLook = true;
        }
    }

    public void GarageNext()
    {
        if (Garage.Count > 0)
        {
            Garage.RemoveAt(0);
            textToShow = Garage[0];
        }
        else if (Garage.Count <= 0)
        { 
            SpeachBox.SetActive(false); 
            Cursor.visible = false;
            canMove = true;
            canLook = true;

        }
    }

    public void WakeUpNext()
    {
        if (wakeUpText.Count > 0)
        {
            wakeUpText.RemoveAt(0);
            textToShow = wakeUpText[0];


        }
        else if (wakeUpText.Count <= 0)
        { 
            SpeachBox.SetActive(false);
            wakeUpTextB.SetActive(false);
            Cursor.visible = false;
            canMove = true;
            canLook = true;

        }
    }

    public void DollFighter()
    {
        if (DollFight.Count > 0)
        {
            DollFight.RemoveAt(0);
            textToShow = DollFight[0];
        }
        else if (DollFight.Count <= 0)
        {
            SpeachBox.SetActive(false);
            Cursor.visible = false;
            canMove = true;
            canLook = true;

        }

    }

    private bool scare;
    public GameObject Rabbit;
    public GameObject Player;
    

 
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

        speachText.text = textToShow;

        
    }

    IEnumerator ScareSounds()
    {
        yield return new WaitForSeconds(0);
        SFXSRCE.PlayOneShot(ScareSound);
        yield return new WaitForSeconds(1);
        SFXSRCE.Stop();

    }

}
