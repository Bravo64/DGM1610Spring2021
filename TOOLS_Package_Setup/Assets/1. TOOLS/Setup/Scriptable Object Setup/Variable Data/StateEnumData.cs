using UnityEngine;

[CreateAssetMenu(fileName = "New State Enum Variable", menuName = "Variable/State Enum")]
public class StateEnumData : ScriptableObject
{
    public enum State{ state0, state1, state2, state3, state4, state5}
    public State value = State.state0;
}
