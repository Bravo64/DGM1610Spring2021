using UnityEngine;
using UnityEngine.Events;

public class MonoDisableEvent : MonoBehaviour
{
    public UnityEvent onDisableEvent;

    private void OnDisable()
    {
        onDisableEvent.Invoke();
    }
    
}
