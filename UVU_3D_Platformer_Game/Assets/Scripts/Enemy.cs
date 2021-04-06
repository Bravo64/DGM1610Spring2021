using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] 
    private Vector3Data playerPositionObj;
    
    private NavMeshAgent _myNavMeshAgent;
    private NavMeshPath _navMeshPath;

    private void Start()
    {
        _myNavMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshPath = new NavMeshPath();
    }

    private void Update()
    {
        _myNavMeshAgent.CalculatePath(playerPositionObj.value, _navMeshPath);
        if (_navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            _myNavMeshAgent.destination = playerPositionObj.value;
        }
        else
        {
            _myNavMeshAgent.destination = transform.position;
        }
    }
}
