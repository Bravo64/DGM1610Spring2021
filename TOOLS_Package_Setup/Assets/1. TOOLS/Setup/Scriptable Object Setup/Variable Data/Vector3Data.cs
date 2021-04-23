using UnityEngine;

[CreateAssetMenu(fileName = "New Vector3 Variable", menuName = "Variable/Vector3")]
public class Vector3Data : ScriptableObject
{
    public Vector3 value;

    public Vector3 GetVector3()
    {
        return value;
    }
    
    public void SetZero()
    {
        value = Vector3.zero;
    }
    
    public void SetVector3(Vector3 input)
    {
        value = input;
    }
    
    public void Add(Vector3 input)
    {
        value += input;
    }
    
    public void Subtract(Vector3 input)
    {
        value -= input;
    }
    
    public void MultiplyBy(float number)
    {
        value *= number;
    }
    
    public void DivideBy(float number)
    {
        value /= number;
    }
}
