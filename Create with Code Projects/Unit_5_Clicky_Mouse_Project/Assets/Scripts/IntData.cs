using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntData : ScriptableObject
{
    public int value;

    public void SetZero()
    {
        value = 0;
    }
    
    public void Increment()
    {
        value++;
    }
    
    public void Add(int number)
    {
        value += number;
    }
}
