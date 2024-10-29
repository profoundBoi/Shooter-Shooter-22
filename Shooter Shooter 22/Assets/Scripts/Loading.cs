using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{

    [SerializeField] private float delayBeforeLoad = 2f;

    private void Start()
    {
        StartCoroutine(LoadSceneAfterDelay("SampleScene"));
    }
       
    private IEnumerator LoadSceneAfterDelay(string SampleScene)
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadSceneAsync(SampleScene);
    }
}
