using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowObjectBehaviour : MonoBehaviour
{
    public Transform targetObject;
    
    private NavMeshAgent _myNavMeshAgent;
    private NavMeshPath _navMeshPath;

    private void Start()
    {
        _myNavMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshPath = new NavMeshPath();
    }

    private void Update()
    {
        _myNavMeshAgent.CalculatePath(targetObject.position, _navMeshPath);
        if (_navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            _myNavMeshAgent.destination = targetObject.position;
        }
        else
        {
            _myNavMeshAgent.destination = transform.position;
        }
    }
}
