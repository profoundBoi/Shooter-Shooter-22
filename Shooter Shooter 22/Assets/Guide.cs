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

            if (Physics.Raycast(ray, out hit, pickUpRange))
            {
                if (hit.collider.CompareTag("Letter"))
                {
                    guideText.text = "'F' To Interact";
                }
                else if (hit.collider.CompareTag("Key"))
                {
                    guideText.text = "'F' To Interact";
                }
                else if (hit.collider.CompareTag("PickUp"))
                {
                    guideText.text = "'E' To Pickup";
                }
                else if (hit.collider.CompareTag("Knife"))
                {
                    guideText.text = "'E' To Pickup";
                }
                else if (hit.collider.CompareTag("Gun"))
                {
                    guideText.text = "'E' To Pickup";
                }
                else if (hit.collider.CompareTag("Syth"))
                {
                    guideText.text = "'E' To Pickup";
                }
                else if (hit.collider.CompareTag("PassKey"))
                {
                    guideText.text = "'E' To Look at PassKey";
                }
                else if (hit.collider.CompareTag("Handle"))
                {
                    guideText.text = "'F' To Interact  ";
                }
                else if (hit.collider.CompareTag("Flash"))
                {
                    guideText.text = "'E' To PickUp  ";
                }

                else
                {
                    guideText.text = "";
                }
            }
        }
        else if (guided) { guideText.text = ""; }

           
        }
        
           

}
    
