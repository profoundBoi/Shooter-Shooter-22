using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider Slider;
    [SerializeField]
    private float Health = 1f;

    [Header("Emergancy Call")]
    [SerializeField] private int MinTime;
    [SerializeField] private int MaxTime;
    public GameObject emergancyCallPanel;
    public bool EmergancyStopped;
    private bool Choke;
    [Header("Scare Audio")]
    public AudioClip Alarm;
    
    [SerializeField]
    AudioSource SFXSRCE;

 

    private void Update()
    {
        Slider.value = Health;

        if (Health <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (EmergancyStopped)
        {
            StopAllCoroutines();
            StartCoroutine(EmergancyCall());
            EmergancyStopped = false;
            emergancyCallPanel.SetActive(false);
            Choke = false;
        }

        if (Choke)
        {
            Health -= 0.00005f;

        }
        else
        {
            if (SFXSRCE.clip == Alarm && !Choke)
            {
                SFXSRCE.clip = Alarm;
                SFXSRCE.clip = null;
                SFXSRCE.Stop();
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spider"))
        {
            Health -= 0.01f;
        }
    }

    private void Start()
    {
        StartCoroutine(EmergancyCall());
        emergancyCallPanel.SetActive(false);
    }

    IEnumerator EmergancyCall()
    {
        yield return new WaitForSeconds(Random.RandomRange(MinTime,MaxTime));
        StartCoroutine(CallEmergancy());
        SFXSRCE.clip = Alarm;
        SFXSRCE.Play();
        yield return new WaitForSeconds(Random.RandomRange(MinTime,MaxTime));
        StartCoroutine(CallEmergancy());

    }

    IEnumerator CallEmergancy()
    {
        emergancyCallPanel.SetActive(true);
        Choke = true;
        yield return new WaitForSeconds(1);
        emergancyCallPanel.SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine(CallEmergancy());



    }

    
}
