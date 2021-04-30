using UnityEngine;

[CreateAssetMenu(fileName = "New Float Variable", menuName = "Variable/Float")]
public class FloatData : ScriptableObject
{
    public float value;

    public float GetFloat()
    {
        return value;
    }
    
    public void SetValue(float number)
    {
        value = number;
    }
    
    public void SetZero()
    {
        value = 0.0f;
    }

    public void Increment()
    {
        value++;
    }
    
    public void Decrement()
    {
        value--;
    }
    
    public void Add(float number)
    {
        value += number;
    }
    
    public void Subtract(float number)
    {
        value += number;
    }
}
