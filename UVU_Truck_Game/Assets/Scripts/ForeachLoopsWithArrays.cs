using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeachLoopsWithArrays : MonoBehaviour
{
    public string[] fruitArray = new string[] {"Apple", "Banana", "Orange", "Pear", "Pineapple"};
    public float holdTime = 1.0f;
    
    private List<string> fruitList = new List<string>();
    private WaitForSeconds _wfsObj;

    IEnumerator Start()
    {
        foreach (var fruit in fruitArray)
        {
            fruitList.Add(fruit);
        }
        
        _wfsObj = new WaitForSeconds(holdTime);
        // These loops can be done with Lists or Arrays.
        // They essentially work the same way.
        // Just assign a keyword to represent
        // the elements inside.
        foreach (string fruit in fruitList)
        {
            yield return _wfsObj;
            print(fruit + " is a fruit.");
        }
    }
}