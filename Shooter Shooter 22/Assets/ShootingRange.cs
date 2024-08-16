using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootingRange : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    private void Start()
    {
        messageText.text = "";
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.CompareTag ("ShootingRange"))
            {
            StartCoroutine(message());
            Destroy(hit.collider);

        }

    }

    IEnumerator message()
    {
        messageText.text = "Welcome To The Shooting Range";
        yield return new WaitForSeconds(2);
        messageText.text = "Pick A knife or Gun To use, on your Left using Square or E";
        yield return new WaitForSeconds(8);
        messageText.text = "Im Assuming you have a Gun now";
        yield return new WaitForSeconds(3);
        messageText.text = "Shoot the Red Block and enjoy using R2 or left Mouse click";
        yield return new WaitForSeconds(2);
        messageText.text = "leTs Go";
        yield return new WaitForSeconds(2);
        messageText.text = "";
    }
}
