using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] 
    private FloatData scrollingSpeedObj;
    
    private float _startScrollingSpeed = 10.0f;
    private bool _assignmentComplete = false;

    void Awake()
    {
        if (!_assignmentComplete)
        {
            scrollingSpeedObj.value = _startScrollingSpeed;
            _assignmentComplete = true;
        }
    }
    
    void LateUpdate()
    {
        transform.Translate(Vector3.left * scrollingSpeedObj.value * Time.deltaTime);

        scrollingSpeedObj.value += 0.05f * Time.deltaTime;
    }
}
