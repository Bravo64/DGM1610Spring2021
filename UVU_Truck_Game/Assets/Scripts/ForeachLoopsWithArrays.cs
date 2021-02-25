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
        foreach (string fruit in fruitList)
        {
            yield return _wfsObj;
            print(fruit + " is a fruit.");
        }
    }
}