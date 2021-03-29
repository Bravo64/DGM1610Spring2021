using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] 
    private Transform platformObject;
    [SerializeField]
    private float movementSpeed = 5.0f;
    
    private List<Transform> waypoints = new List<Transform>();
    private int _currentWaypoint;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                waypoints.Add(child);
            }
        }
        _currentWaypoint = Random.Range(0, waypoints.Count);
    }
    
    void Update()
    {
        platformObject.position = Vector3.MoveTowards(platformObject.position, waypoints[_currentWaypoint].position, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(platformObject.position, waypoints[_currentWaypoint].position) < 0.1f)
        {
            _currentWaypoint = Random.Range(0, waypoints.Count);
        }
    }
}
