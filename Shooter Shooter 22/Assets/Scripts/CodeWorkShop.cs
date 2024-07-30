using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeWorkShop : MonoBehaviour
{
    const int sgebiSpeed = 10; // this keeps the value constant

    int num = 6;

    int a = 5;
    int b = 6;

    float c = 5.0f;
    float d = 6.0f;

    string firstName = "Dollapho";
    string secondName = "Van Heerden";

    float soapPrice = 372;
    float towelPrice = 1047;

  
    void Start()
    {
        //We are storing the score of 5 and debugging/printing it
        int score;  //or int score = 5;
        score = 5;  //   print(score);
        print(score); //this shows the score in the console

        //now we are storing the score of 6
        score = 6;
        print(score);

        float gravity;
        gravity = 9.8f;
        Debug.Log(gravity);// you can also use print

        num += 7; //or num = num + 7; note that num is now 13 

        int sum = a + b;// or Debug.Log(a + b);    
        Debug.Log(sum);
        int min = a - b;// or Debug.Log(a - b);
        Debug.Log(min);
        float div = a / b;// or Debug.Log(a / b);
        Debug.Log(div);

        float divi = c / d;// or Debug.Log(c / d);
        Debug.Log(divi);

        Debug.Log("Hello"+ "  " + firstName + "  " + secondName);

        Debug.Log(soapPrice % 2);
        Debug.Log(towelPrice % 2);

       
        
    }

  
    void Update()
    {
        
    }
}
