using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartButtonClicked()
    {
        SceneManager.LoadSceneAsync("Story");
        Debug.Log("m mmmmmmmm");
    }

    public void NextButtonClicked()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }


    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
