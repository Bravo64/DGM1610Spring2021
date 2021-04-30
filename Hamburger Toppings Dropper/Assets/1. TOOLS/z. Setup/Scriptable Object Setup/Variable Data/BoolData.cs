using UnityEngine;

[CreateAssetMenu(fileName = "New Color Variable", menuName = "Variable/Color")]
public class BoolData : ScriptableObject
{
    public bool value;

    public bool GetBool()
    {
        return value;
    }
    
    public void SetTrue()
    {
        value = true;
    }
    
    public void SetFalse()
    {
        value = false;
    }
    
    public void ToggleBool()
    {
        value = !value;
    }
}
