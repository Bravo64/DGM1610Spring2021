using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEventBehaviour : MonoBehaviour
{
    public float delayTime = 1.0f;
    public bool beginOnStart = true;
    public UnityEvent delayedEvent;

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
