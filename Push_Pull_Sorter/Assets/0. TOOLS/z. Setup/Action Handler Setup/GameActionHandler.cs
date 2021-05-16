using UnityEngine;
using UnityEngine.Events;

public class GameActionHandler : MonoBehaviour
{
    public GameAction gameActionObj;
    public UnityEvent onRaiseEvent;
    
    void Start()
    {
        gameActionObj.action += ActionHandler;
    }

    private void ActionHandler()
    {
        onRaiseEvent.Invoke();
    }
}