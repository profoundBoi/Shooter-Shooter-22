using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip Clip;

    private void Start()
    {
        Source.clip = Clip;

        // Enable looping
        Source.loop = true;

        // Play the audio
        Source.Play();
    }
}
