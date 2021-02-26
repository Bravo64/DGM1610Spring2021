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
            // These loops can be done with Lists or Arrays.
            // They essentially work the same way.
            // Use list[i] / array[i] to grab an item.
            print(fruitList[i] + " is a fruit.");
        }
    }
}
