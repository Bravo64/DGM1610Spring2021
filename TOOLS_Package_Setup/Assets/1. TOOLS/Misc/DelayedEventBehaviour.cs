using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEventBehaviour : MonoBehaviour
{
    [SerializeField] 
    private float delayTime = 1.0f;
    [SerializeField] 
    private bool beginOnStart = true;
    [SerializeField]
    private UnityEvent delayedEvent;

    private WaitForSeconds _waitForSecondsObj;

    void Start()
    {
        _waitForSecondsObj = new WaitForSeconds(delayTime);
        if (beginOnStart)
        {
            InitiateCountdown();
        }
    }
    
    public void InitiateCountdown()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return _waitForSecondsObj;
        delayedEvent.Invoke();
    }
}
