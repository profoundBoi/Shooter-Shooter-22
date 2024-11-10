using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public void ExitButtonClicked()
    {
        Application.Quit();
    }

    public void Restart1ButtonClicked()
    {
        SceneManager.LoadSceneAsync("Laoding");
    }
}
