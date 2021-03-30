using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOutOfBoundObj : MonoBehaviour
{
    [SerializeField]
    private Transform leftBoundaryMarker;

    private float _leftBoundaryPosX;
    
    void Start()
    {
        _leftBoundaryPosX = leftBoundaryMarker.position.x;
    }
    
    void Update()
    {
        if (transform.position.x <= _leftBoundaryPosX)
        {
            gameObject.SetActive(false);
        }
    }
}
