using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerScript : MonoBehaviour
{
    public Light speakerlight;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightPlay() );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LightPlay()
    {
        speakerlight.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(LightPlay());
    }
}
