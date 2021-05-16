using UnityEngine;
using UnityEngine.Events;

public class CheckObjectActiveBehaviour : MonoBehaviour
{
    public enum AssertionTypes { IsActive, IsNotActive }
    
    public bool checkOnStart = true;
    public GameObject objectToCheck;
    public AssertionTypes checkFor = AssertionTypes.IsActive;
    public UnityEvent stateIsTrueEvent;

    private void Start()
    {
        if (checkOnStart)
        {
            CheckObjectState();
        }
    }

    public void CheckObjectState()
    {
        switch (checkFor)
        {
            case AssertionTypes.IsActive:
                if (!objectToCheck.activeSelf) { return;}
                break;
            case AssertionTypes.IsNotActive:
                if (objectToCheck.activeSelf) { return;}
                break;
        }
        stateIsTrueEvent.Invoke();
    }
}
