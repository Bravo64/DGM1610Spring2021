using UnityEngine;

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
        value += number;
    }
}
