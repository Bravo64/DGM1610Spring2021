using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScript : MonoBehaviour
{
    // Public variables can be changed in the editor
    public string myName;
    
    // Start is called before the first frame update
    void Start()
    {
        // Print a message to the console
        Debug.Log("Hello Beautiful World!");
        Debug.Log("My name is" + myName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
