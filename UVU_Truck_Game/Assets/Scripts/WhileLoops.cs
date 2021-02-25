using System.Collections;
using UnityEngine;

public class WhileLoops : MonoBehaviour
{
    public float holdTime = 1.0f;
    public float maxNumber = 10;
    
    private int _counter;
    private WaitForSeconds _wfsObj;

    IEnumerator Start()
    {
        _wfsObj = new WaitForSeconds(holdTime);
        while (_counter < maxNumber)
        {
            _counter++;
            yield return _wfsObj;
            print(_counter);
        }
    }
}
