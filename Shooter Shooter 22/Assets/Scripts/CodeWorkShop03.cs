using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CodeWorkShop03 : MonoBehaviour
{
    int level = 0;
    string[] brothers = { "Mario", "Luigi", "Toad" };
    int[] ammo = {  30, 10, 25 };
    int count = 0;
    
    void Start()
    {
        while( level < 10)
        {
            level++;
        }
        print(level);

        for(int i = 0; i < 3; i++)
        {
            print(brothers[i]);
        }

        for(int k = 2; k >= 0; k--)
        {
            print(brothers[k]);
        }

        while ( count < 3 )
        {
            print(ammo[count]);
            count++;
        }

        for (int j = 0; j < ammo.Length; j++)
        {
            print(ammo);
        }

        foreach ( int ammo in ammo)
        {
            print(ammo);
        }

        print(ammo[0]);
        print(ammo[1]);
        print(ammo[2]);

    }

    
    void Update()
    {
        
    }
}
