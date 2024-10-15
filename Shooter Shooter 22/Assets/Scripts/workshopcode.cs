using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;
using System.Linq;
public class workshopcode : MonoBehaviour
{
    Dictionary<int, string> Names = new Dictionary<int, string>();
    private void Start()
    {
        Names.Add(1,"Rea");
        Names.Add(2, "Flame");
        Names.Add(3, "Flamer");
        Names.Add(4, "Flamed");
        Names.Add(5, "Flames");


        Debug.Log(Names.ElementAt(0).Value);
        Debug.Log(Names.ElementAt(0).Key);
    }

    private void Update ()
    {
       
    }

}
