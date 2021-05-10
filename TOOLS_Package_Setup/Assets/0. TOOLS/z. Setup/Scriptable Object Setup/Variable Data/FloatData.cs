using UnityEngine;
using Random = UnityEngine.Random;


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
    
    public void Randomize(float boundary)
    {
        value = Random.Range(value - boundary, value + boundary);
    }
    
    public void AddRandom(float maxAddition)
    {
        value = Random.Range(value, value + maxAddition);
    }
    
    public void SubtractRandom(float maxSubtraction)
    {
        value = Random.Range(value - maxSubtraction, value);
    }
    
    public void RandomMinZero(float maxValue)
    {
        value = Random.Range(0.0f, maxValue);
    }
    
    public void RandomMinOne(float maxValue)
    {
        value = Random.Range(1.0f, maxValue);
    }
}
