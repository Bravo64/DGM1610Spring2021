using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RoadblockMovement : MonoBehaviour
{
    [SerializeField]
    private Transform barrier;
    [SerializeField]
    private float movementSpeed = 5.0f;
    
    private List<Transform> waypoints = new List<Transform>();
    private int _activeWaypoint = 0;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                waypoints.Add(child);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        barrier.position = Vector3.MoveTowards(barrier.position, waypoints[_activeWaypoint].position, movementSpeed * Time.deltaTime);
        if (Vector3.Distance(barrier.position, waypoints[_activeWaypoint].position) < 0.01f)
        {
            if (_activeWaypoint < waypoints.Count - 1)
            {
                _activeWaypoint++;
            }
            else
            {
                _activeWaypoint = 0;
            }
        }
    }
}
