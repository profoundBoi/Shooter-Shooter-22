using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CodeWorkShop02 : MonoBehaviour
{
    string name = "Dollapho";//we are identifying the name as Dollapho

    int a = 8;
    int b = 12;

    public int c = 7;

    public int playerScore = 100;
    
    void Start()
    {
        if( name == "Dollapho")// this is saying if the name is Dollapho do what the statement below says
        {
            print("Hello Dollapho");// we print the name Dollapho it is true that the name is Dollapho
        }
        else
        {
            print("Hello Stranger");// if name is not Dollapho we then print stranger
        }

        if( a > b )
        {
            print(" A is larger than B ");
        }
        else
        {
            print(" A is smaller than B ");
        }

        //print if the number "C" is smaller, greater or equal to 8
        if (c > 8)
        {
            print("C is greater than 8");
        }
        else if (c == 8)
        {
            print("C is equal to 8");
        }
        else if (c < 8)
        {
            print("C is smaller than 8");
        }

        // we print the player score
        if ( playerScore >= 0 && playerScore <= 2)
        {
            print("You suck at the game");
        }
        else if( playerScore >= 3 && playerScore <= 4)
        {
            print("you are ok at this game");
        }
        else if(playerScore >= 5 && playerScore <= 6)
        {
            print("you are good at this game");
        }
        else if(playerScore >= 7 && playerScore <= 10)
        {
            print("you are great at this game");
        }
        
        
    }

    
    void Update()
    {
        
    }
}
