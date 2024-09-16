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

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.CompareTag("MirrorScare"))
        {
            Destroy(hit.collider);
            GameObject Monster = Instantiate(ScaryLongLegs, ScaryLegsPosition.position, Quaternion.identity);
            Destroy(Monster);

        }

    }

    
}
