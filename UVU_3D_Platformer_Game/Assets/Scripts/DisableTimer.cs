using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTimer : MonoBehaviour
{
    [SerializeField] 
    private float disableWaitTime = 2.0f;

    private WaitForSeconds _waitForSecondsObj;

    private void Awake()
    {
        _waitForSecondsObj = new WaitForSeconds(disableWaitTime);
    }
    
    private void OnEnable()
    {
        StartCoroutine(DisableCountdown());
    }
    
    IEnumerator DisableCountdown()
    {
        yield return _waitForSecondsObj;
        
        gameObject.SetActive(false);
    }
}
