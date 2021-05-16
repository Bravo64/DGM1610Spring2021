using UnityEngine;
using UnityEngine.Events;

public class CheckIntDataBehaviour : MonoBehaviour
{
    public enum Modes { CompareMainToSecondary, CompareMainToVariable}
    public enum AssertionTypes { IsEqualTo, IsGreaterThan, IsLessThan, IsGreaterThanOrEqualTo, IsLessThanOrEqualTo}

    public bool checkOnStart = true;
    public Modes mode = Modes.CompareMainToSecondary;
    public IntData mainIntData;
    public AssertionTypes checkFor = AssertionTypes.IsEqualTo;
    public IntData secondaryIntData;
    public int intVariable;
    public UnityEvent ifStatementIsTrueEvent;

    private int _comparisonValue;

    void Start()
    {
        if (checkOnStart)
        {
            CheckIfTrue();
        }   
    }
    
    public void CheckIfTrue()
    {
        if (mode == Modes.CompareMainToSecondary)
        {
            _comparisonValue = secondaryIntData.value;
        }
        else
        {
            _comparisonValue = intVariable;
        }
        
        switch (checkFor)
        {
            case AssertionTypes.IsEqualTo:
                if (mainIntData.value != _comparisonValue) { return; }
                break;
            case AssertionTypes.IsGreaterThan:
                if (mainIntData.value <= _comparisonValue) { return; }
                break;
            case AssertionTypes.IsLessThan:
                if (mainIntData.value >= _comparisonValue) { return; }
                break;
            case AssertionTypes.IsGreaterThanOrEqualTo:
                if (mainIntData.value < _comparisonValue) { return; }
                break;
            case AssertionTypes.IsLessThanOrEqualTo:
                if (mainIntData.value > _comparisonValue) { return; }
                break;
        }
        ifStatementIsTrueEvent.Invoke();
    }
}