using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Integer Variable", menuName = "Variable/Integer")]
public class IntData : ScriptableObject
{
    public int value;

    public int GetInt()
    {
        return value;
    }
    
    public void SetValue(int number)
    {
        value = number;
    }
    
    public void SetZero()
    {
        value = 0;
    }

    public void Increment()
    {
        value++;
    }
    
    public void Decrement()
    {
        value--;
    }
    
    public void Add(int number)
    {
        value += number;
    }
    
    public void Subtract(int number)
    {
        value -= number;
    }

    public void Randomize(int boundary)
    {
        value = Random.Range(value - boundary, value + boundary);
    }
    
    public void AddRandom(int maxAddition)
    {
        value = Random.Range(value + 1, value + maxAddition + 1);
    }
    
    public void SubtractRandom(int maxSubtraction)
    {
        value = Random.Range(value - maxSubtraction, value);
    }
    
    public void RandomMinZero(int maxValue)
    {
        value = Random.Range(0, maxValue);
    }
    
    public void RandomMinOne(int maxValue)
    {
        value = Random.Range(1, maxValue);
    }
}
