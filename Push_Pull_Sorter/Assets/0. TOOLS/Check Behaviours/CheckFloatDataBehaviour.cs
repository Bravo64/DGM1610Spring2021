using UnityEngine;
using UnityEngine.Events;

public class CheckFloatDataBehaviour : MonoBehaviour
{
    public enum Modes { CompareMainToSecondary, CompareMainToVariable}
    public enum AssertionTypes { IsEqualTo, IsGreaterThan, IsLessThan, IsGreaterThanOrEqualTo, IsLessThanOrEqualTo}

    public bool checkOnStart = true;
    public Modes mode = Modes.CompareMainToSecondary;
    public FloatData mainFloatData;
    public AssertionTypes checkFor = AssertionTypes.IsEqualTo;
    public FloatData secondaryFloatData;
    public float floatVariable;
    public UnityEvent ifStatementIsTrueEvent;

    private float _comparisonValue;

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
            _comparisonValue = secondaryFloatData.value;
        }
        else
        {
            _comparisonValue = floatVariable;
        }
        
        switch (checkFor)
        {
            case AssertionTypes.IsEqualTo:
                if (mainFloatData.value != _comparisonValue) { return; }
                break;
            case AssertionTypes.IsGreaterThan:
                if (mainFloatData.value <= _comparisonValue) { return; }
                break;
            case AssertionTypes.IsLessThan:
                if (mainFloatData.value >= _comparisonValue) { return; }
                break;
            case AssertionTypes.IsGreaterThanOrEqualTo:
                if (mainFloatData.value < _comparisonValue) { return; }
                break;
            case AssertionTypes.IsLessThanOrEqualTo:
                if (mainFloatData.value > _comparisonValue) { return; }
                break;
        }
        ifStatementIsTrueEvent.Invoke();
    }
}
