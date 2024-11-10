using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource Source, Source2;
    public AudioClip Clip, Clip2;

    private void Start()
    {
        Source.clip = Clip;

        // Enable looping
        Source.loop = true;

        // Play the audio
        Source.Play();

        StartCoroutine(Playwhispers());
    }

   IEnumerator Playwhispers()
    {
        yield return new WaitForSeconds(Random.Range(90, 140));
        Source2.clip = Clip2;

        // Enable looping
        Source2.loop = true;

        // Play the audio
        Source2.Play();
        StartCoroutine(Playwhispers());

    }
}
