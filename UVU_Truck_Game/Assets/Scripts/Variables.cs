using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    private int _integerVariable = 123;
    private double _doubleVariable = 19.99;
    private char _characterVariable = 'A';
    private string _stringVariable = "Hello World";
    private bool _booleanVariable = true;
    
    // Start is called before the first frame update
    void Start()
    {
        print(_integerVariable);
        print(_doubleVariable);
        print(_characterVariable);
        print(_stringVariable);
        print(_booleanVariable);
    }
    
}
