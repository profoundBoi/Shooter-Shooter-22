using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject ControlPanel;
    public GameObject ControlPanel02;
    public GameObject PauseButton;
    public GameObject PausePanel;
    public GameObject SettingsPanel;
    public GameObject LoadingPage;
    public void PausedButtonClicked()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0; //i am pausing the state at which the game runs
        PauseButton.SetActive(false);
    }

    public void PlayButtonClicked()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        PauseButton.SetActive(true);
    }
    public void StartButtonClicked()
    {
        SceneManager.LoadSceneAsync("Story");
        Debug.Log("Loading Story");
    }

    public void Next01ButtonClicked()
    {
        SceneManager.LoadSceneAsync("Story2");
    }
    public void Next02ButtonClicked()
    {
        SceneManager.LoadSceneAsync("SampleScene");
        LoadingPage.SetActive(true);
    }

    public void Prev01ButtonClicked()
    {
        SceneManager.LoadSceneAsync("Story");
    }

    public void ControlsButtonClicked()
    {
        ControlPanel.SetActive(true);
    }

    public void ControlsButton02Clicked()
    {
        ControlPanel02.SetActive(true);
    }

    public void ControlsExit01Clicked()
    {
        ControlPanel.SetActive(false);
    }

    public void ControlsExit02Clicked()
    {
        ControlPanel02.SetActive(false);
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
