using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public Transform playerCamera;
    public int pickUpRange;
    public TextMeshProUGUI guideText;
    public GameObject Letter1;
    public GameObject Letter2;
    public GameObject Letter3;

    public bool guided;
    public LayerMask Interact;
    public LayerMask PU;
    public LayerMask Look;
    public LayerMask Keys;
    public GameObject GuideBG;
    // Start is called before the first frame update
    void Start()
    {
        guideText.text = "";
    }
    // Update is called once per frame
    void Update()
    {

        if (Letter1.activeSelf || Letter2.activeSelf || Letter3.activeSelf)
        {
            guided = true;
        }
        else { guided = false; }



        if (!guided)
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pickUpRange, Interact))
            {
                GameObject Object = hit.collider.gameObject;
                guideText.text = hit.collider.name + " - Click 'F' To Interact";
                GuideBG.SetActive(true);
            }
            else if (Physics.Raycast(ray, out hit, pickUpRange, Look))
            {
                GameObject Object = hit.collider.gameObject;
                guideText.text = hit.collider.name;
                GuideBG.SetActive(true);

            }
            else if (Physics.Raycast(ray, out hit, pickUpRange, PU))
            {
                GameObject Object = hit.collider.gameObject;
                guideText.text = hit.collider.name + " - Click 'E' To PickUp";
                GuideBG.SetActive(true);

            }
            else if (Physics.Raycast(ray, out hit, pickUpRange, Keys))
            {
                GameObject Object = hit.collider.gameObject;
                guideText.text = hit.collider.name + " - Left Click To Enter Pin";
                GuideBG.SetActive(true);

            }

            else { 
                guideText.text = ""; 
                GuideBG.SetActive(false);
                }
        }
        else if (guided) { guideText.text = ""; }

           
        }
        
           

}
    
