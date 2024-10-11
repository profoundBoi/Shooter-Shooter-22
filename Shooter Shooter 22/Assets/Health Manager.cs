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

    private void Update()
    {
        Slider.value = Health;

        if (Health <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spider"))
        {
            Health -= 0.04f;
        }
    }

}
