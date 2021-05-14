using UnityEngine;

[CreateAssetMenu(fileName = "New State Enum Variable", menuName = "Variable/State Enum")]
public class StateEnumData : ScriptableObject
{
    public enum State{ State0, State1, State2, State3, State4, State5}
    public State value = State.State0;
    
    public void SetEnumState(int input)
    {
        switch (input)
        {
            case 0:
                value = State.State0;
                break;
            case 1:
                value = State.State1;
                break;
            case 2:
                value = State.State2;
                break;
            case 3:
                value = State.State3;
                break;
            case 4:
                value = State.State4;
                break;
            case 5:
                value = State.State5;
                break;
            default:
                value = State.State0;
                break;
        }
    }
}
