using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RandomDelayBehaviour : MonoBehaviour
{
    public float minDelayTime, maxDelayTime;
    public bool beginOnStart = true;
    public UnityEvent delayedEvent;

    private WaitForSeconds _waitForSecondsObj;

    void Start()
    {
        _waitForSecondsObj = new WaitForSeconds(Random.Range(minDelayTime, maxDelayTime));
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
