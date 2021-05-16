using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventRepeaterBehaviour : MonoBehaviour
{
    public float intervalDelay = 1.0f;
    public bool beginOnStart = true;
    public UnityEvent repeatingEvent;

    private WaitForSeconds _waitForSecondsObj;

    void Start()
    {
        _waitForSecondsObj = new WaitForSeconds(intervalDelay);
    }

    private void OnEnable()
    {
        if (beginOnStart)
        {
            InitiateRepeater();
        }
    }

    public void InitiateRepeater()
    {
        StartCoroutine(WaitAndRepeat());
    }

    IEnumerator WaitAndRepeat()
    {
        while (true)
        {
            yield return _waitForSecondsObj;
            repeatingEvent.Invoke();
        }
    }

    private void OnDisable()
    {
        PauseRepeater();
    }

    public void PauseRepeater()
    {
        StopAllCoroutines();
    }
}