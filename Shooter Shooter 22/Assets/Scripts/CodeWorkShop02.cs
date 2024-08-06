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

    int number = 3;

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

        switch(name)
        {
            case "Dollapho":
                print("Hello Dollapho");
                break;
            default:    
                print("Hello Stranger");
                break;
        }

        switch (number)
        {
            case 0:
                print("zero");
                break;
            case 1:
                print("one");
                break;
            case 2:
                print("two");
                break;
            case 3:
                print("three");
                break;
            case 4:
                print("four");
                break;
            case 5:
                print("five");
                break;
        }

        switch (playerScore)
        {
            case 0:
            case 1:
            case 2:
                print("you suck at this game");
                break;
            case 3:
            case 4:
                print("you are ok at this game");
                break;
            case 5:
            case 6:
                print("you are good at this game");
                break;
            case 7:
            case 8:
            case 9:
            case 10:
                print("you are great at this game");
                break;

                
        }
        
        
    }

    
    void Update()
    {
        
    }
}
