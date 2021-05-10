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
    
    public void Randomizer(float boundary)
    {
        value = Random.Range(value - boundary, value + boundary);
    }
    
    public void AdditionRandomizer(float maxAddition)
    {
        value = Random.Range(value, value + maxAddition);
    }
    
    public void SubtractionRandomizer(float maxSubtraction)
    {
        value = Random.Range(value - maxSubtraction, value);
    }
    
    public void RandomizerFloorZero(float maxValue)
    {
        value = Random.Range(0.0f, maxValue);
    }
    
    public void RandomizerFloorOne(float maxValue)
    {
        value = Random.Range(1.0f, maxValue);
    }
}
