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

    public void PausedButtonClicked()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
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
    }

    public void Prev01ButtonClicked()
    {
        SceneManager.LoadSceneAsync("Story");
    }

    public void ControlsButtonClicked()
    {
        ControlPanel.SetActive(true);
    }

    public void ControlsExit01Clicked()
    {
        ControlPanel.SetActive(false);
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
