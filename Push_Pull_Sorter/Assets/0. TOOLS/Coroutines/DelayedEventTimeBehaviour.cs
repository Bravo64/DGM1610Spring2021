using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEventTimeBehaviour : MonoBehaviour
{
    public float delayTime = 1.0f;
    public bool beginOnEnable = true;
    public UnityEvent delayedTimeEvent;

    private WaitForSeconds _waitForSecondsObj;

    void Awake()
    {
        _waitForSecondsObj = new WaitForSeconds(delayTime);
    }

    private void OnEnable()
    {
        if (beginOnEnable)
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
        delayedTimeEvent.Invoke();
    }
}
