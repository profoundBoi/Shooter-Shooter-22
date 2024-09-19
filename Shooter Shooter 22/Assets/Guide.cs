using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public Transform playerCamera;
    public int pickUpRange;
    public TextMeshProUGUI guideText;

    // Start is called before the first frame update
    void Start()
    {
        guideText.text = "";
    }
    // Update is called once per frame
    void Update()
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
            else if (hit.collider.CompareTag("Bottle"))
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

            else
            {
                guideText.text = "";
            }
        }
        
    }

}
    
