using UnityEngine;

[CreateAssetMenu(fileName = "New String Variable", menuName = "Variable/String")]
public class StringData : ScriptableObject
{
    [TextArea(10,15)]
    public string value;

    public string GetString()
    {
        return value;
    }
    
    public void SetString(string input)
    {
        value = input;
    }
    
    public void Clear()
    {
        value = "";
    }

    public void AppendToString(string input)
    {
        value += input;
    }
    
    public void InsertString(string input)
    {
        value = input + value;
    }
}
