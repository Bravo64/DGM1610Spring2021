using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeater : MonoBehaviour
{
    [SerializeField] 
    private Transform leftBoundaryMarker;

    private float _startPosX;
    private float _leftBoundsPosX;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPosX = transform.position.x;
        _leftBoundsPosX = leftBoundaryMarker.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= _leftBoundsPosX)
        {
            Vector3 _myPosition = transform.position;
            _myPosition.x = _startPosX;
            transform.position = _myPosition;
        }
    }
}
