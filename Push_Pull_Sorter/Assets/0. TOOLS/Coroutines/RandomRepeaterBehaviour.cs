using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomRepeaterBehaviour : MonoBehaviour
{
    public float minimumDelay, maximumDelay;
    public bool beginOnStart = true;
    public UnityEvent repeatingEvent;

    private List<WaitForSeconds> _waitForSecondsObjs = new List<WaitForSeconds>();

    void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            _waitForSecondsObjs.Add(new WaitForSeconds(Random.Range(minimumDelay, maximumDelay)));
        }
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
            yield return _waitForSecondsObjs[Random.Range(0, _waitForSecondsObjs.Count)];
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