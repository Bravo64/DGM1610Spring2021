using UnityEngine;
using UnityEngine.Events;

public class MonoEnableEvent : MonoBehaviour
{
    public UnityEvent onEnableEvent;

    private void OnEnable()
    {
        onEnableEvent.Invoke();
    }
    
}
