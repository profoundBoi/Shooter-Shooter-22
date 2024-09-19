using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeachScript : MonoBehaviour
{
    public TextMeshProUGUI speachText;
    public GameObject SpeachBox;

    [Header("=Scarey LongLegs")]
    public GameObject ScaryLongLegs;
    public Transform ScaryLegsPosition;
    public Transform ScaryLegsposition2;
    // Start is called before the first frame update
    void Start()
    {
        SpeachBox.SetActive(false);
        StartCoroutine(WakeUp());
    }

    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(13);
        SpeachBox.SetActive(true);
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
            



        }

       

    }

    IEnumerator DoorScare()
    {
        yield return new WaitForSeconds(1);
        SpeachBox.SetActive(true);
        speachText.text = "What was that";
        yield return new WaitForSeconds(4);
        speachText.text = "This Place is not Safe no more";
        yield return new WaitForSeconds(4);
        speachText.text = "Let me grab Dads weapon from the Safe";
        yield return new WaitForSeconds(3);
        speachText.text = "";
        SpeachBox.SetActive(false);



    }


}
