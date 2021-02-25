using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForLoopsWithArrays : MonoBehaviour
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
        for (var i = 0; i < fruitList.Count; i++)
        {
            yield return _wfsObj;
            print(fruitList[i] + " is a fruit.");
        }
    }
}
