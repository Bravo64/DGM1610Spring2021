using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditionals : MonoBehaviour
{
    public int messageOption = 5;


    void Start()
    {
        // Switch statement has multiple options based on cases.
        if (messageOption == 1)
        {
            print("Hello there.");
            if (messageOption == 2)
            {
                print("What's up?");
            }

            else if (messageOption == 3)
            {
                print("How are you?");
            }

            else if (messageOption == 4)
            {
                print("Great weather we're having.");
            }

            else if (messageOption == 5)
            {
                print("Salutations!");
            }
            else
            {
                print("I don't know what to say.");
            }
        }
    }
}