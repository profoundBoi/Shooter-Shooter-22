using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutManager : MonoBehaviour
{
    public GameObject SpiderTut;
    public GameObject BunnyTut;
    public GameObject BatTut;
    public GameObject KeyMakerTut;
    public GameObject EndGameTut;

    public void Next01ButtonClicked()
    {
        SpiderTut.SetActive(false);
        Time.timeScale = 1;
        BunnyTut.SetActive(true);
    }

    public void Next02ButtonClicked()
    {
        BunnyTut.SetActive(false);
        Time.timeScale = 1;
        BatTut.SetActive(true);
    }

    public void Next03ButtonClicked()
    {
        BatTut.SetActive(false);
        Time.timeScale = 1;
        KeyMakerTut.SetActive(true);
    }

    public void Next04ButtonClicked()
    {
        KeyMakerTut.SetActive(false);
        Time.timeScale = 1;
        EndGameTut.SetActive(true);
    }

    public void Next05ButtonClicked()
    {
        EndGameTut.SetActive(false);
        Time.timeScale = 1;
        SpiderTut.SetActive(true);
    }
}
