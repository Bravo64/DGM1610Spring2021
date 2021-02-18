using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switches : MonoBehaviour
{
    public int messageOption = 5;


    void Start()
    {
        // Switch statement has multiple options based on cases.
        switch (messageOption)
        {
            case 1:
                print("Hello there.");
                break;
            case 2:
                print("What's up?");
                break;
            case 3:
                print("How are you?");
                break;
            case 4:
                print("Great weather we're having.");
                break;
            case 5:
                print("Salutations!");
                break;
            default:
                print("I don't know what to say.");
                break;
        }
    }
}
