using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New State Enum Variable", menuName = "Variable/State Enum")]
public class StateEnumData : ScriptableObject
{
    public enum State{ state0, state1, state2, state3, state4, state5}
    public State value = State.state0;
    
    public void SetEnumState(int input)
    {
        switch (input)
        {
            case 0:
                value = State.state0;
                break;
            case 1:
                value = State.state1;
                break;
            case 2:
                value = State.state2;
                break;
            case 3:
                value = State.state3;
                break;
            case 4:
                value = State.state4;
                break;
            case 5:
                value = State.state5;
                break;
            default:
                value = State.state0;
                break;
        }
    }
}
