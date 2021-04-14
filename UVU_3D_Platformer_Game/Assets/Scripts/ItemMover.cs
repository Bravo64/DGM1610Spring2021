using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemMover : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5.0f;
    [SerializeField]
    private float speedRandomizer = 0.0f;
    [SerializeField]
    private Transform pickupItem;
    enum CycleType{ pingPong, loop}
    [SerializeField] 
    CycleType movementCycleType = CycleType.pingPong;
    
    private List<Transform> _waypoints = new List<Transform>();
    private int _activeWaypoint = 0;
    private int _movementDirection = 1;
    private bool _scaleResetComplete = false;
    
    void Start()
    {
        movementSpeed += Random.Range(-speedRandomizer, speedRandomizer);
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                _waypoints.Add(child);
            }
        }
    }
    
    void Update()
    {
        pickupItem.position = Vector3.MoveTowards(pickupItem.position, _waypoints[_activeWaypoint].position,
            movementSpeed * Time.deltaTime);
        if (Vector3.Distance(pickupItem.position, _waypoints[_activeWaypoint].position) < 0.01f)
        {
            if (_activeWaypoint >= _waypoints.Count - 1)
            {
                _activeWaypoint = _waypoints.Count - 1;
                if (movementCycleType == CycleType.pingPong)
                {
                    _movementDirection = -1;
                }
                else if (movementCycleType == CycleType.loop)
                {
                    _activeWaypoint = -1;
                }
            }
            else if (_activeWaypoint <= 0 && movementCycleType == CycleType.pingPong)
            {
                _activeWaypoint = 0;
                _movementDirection = 1;
            }
            _activeWaypoint += _movementDirection;
        }
    }
}
