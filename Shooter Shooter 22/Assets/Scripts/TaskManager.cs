using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public FirstPersonControl firstPersonControlScript;
    public TextMeshProUGUI taskText;

    public GameObject Check;
    public Transform checkBox;
    [SerializeField]
    private List <string> Tasks = new List <string>();

    private bool Task1, Task2, Task3, Task4, Task5, Task6 = false;
    public SpeachScript SpeachScriptTask;
    // Start is called before the first frame update
    void Start()
    {

        Tasks.Add("Grab a Flash");
        Tasks.Add("Get a Weapon");
        Tasks.Add("Kill 5 Spiders");
        Tasks.Add("");
        Tasks.Add("");
        Tasks.Add("");
        Tasks.Add("");
        Tasks.Add("");

    }

    // Update is called once per frame
    void Update()
    {
        taskText.text = Tasks[0];
        if (firstPersonControlScript.holdingFlash && Task1 == false)
        {
            taskText.gameObject.SetActive(true);
            Task1 = true;
            StartCoroutine(TaskComplete());
        }
        if (SpeachScriptTask.GetGun)
        {
            taskText.gameObject.SetActive (true);
        }
        if (firstPersonControlScript.holdingGun && Task2 == false)
        {
            Task2 = true;
            StartCoroutine(TaskComplete());
        }
        else if (firstPersonControlScript.holdingKnife && Task2 == false)
        {
            Task2 = true;
            StartCoroutine(TaskComplete());
        }
        else if (firstPersonControlScript.holdingSyth && Task2 == false)
        {
            Task2 = true;
            StartCoroutine(TaskComplete());
        }


    }

    IEnumerator TaskComplete()
    {
        GameObject TaskDone = Instantiate(Check, checkBox.position, Quaternion.identity);
        TaskDone.transform.parent = checkBox.transform;
        yield return new WaitForSeconds(1);
        Tasks.RemoveAt(0);
        taskText.gameObject.SetActive(false);
        Destroy(TaskDone);
        
    }
}
