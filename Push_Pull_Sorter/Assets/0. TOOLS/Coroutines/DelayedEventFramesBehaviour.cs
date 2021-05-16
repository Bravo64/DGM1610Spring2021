using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEventFramesBehaviour : MonoBehaviour
{
    public int numberOfWaitFrames = 1;
    public bool beginOnStart = true;
    public UnityEvent delayedFramesEvent;

    void Start()
    {
        if (beginOnStart)
        {
            InitiateFrameWait();
        }
    }
    
    public void InitiateFrameWait()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return numberOfWaitFrames - 1;
        delayedFramesEvent.Invoke();
    }
}
