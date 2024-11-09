using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFix : MonoBehaviour
{
    public GameObject ShoulderLeft;
    public GameObject ShoulderRight;
    public Transform Cam;
    public FirstPersonControl FirstPersonControl;

    // Update is called once per frame
    void Update()
    {
        ShoulderLeft.transform.eulerAngles = new Vector3(transform.rotation.x, FirstPersonControl.playerCamera.localEulerAngles.x + 90, transform.rotation.z);
    }
}
