using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntData : ScriptableObject
{
    public int value;

    public void Increment()
    {
        value++;
    }
    
    public void Decrement()
    {
        value--;
    }
    
    public int GetValue()
    {
        return value;
    }
    
    public void SetValue(int newValue)
    {
        value = newValue;
    }
    
    public void SetZero()
    {
        value = 0;
    }
}
