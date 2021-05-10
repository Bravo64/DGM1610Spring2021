using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowVector3DataBehaviour : MonoBehaviour
{
    public Vector3Data targetVector3Data;
    
    private NavMeshAgent _myNavMeshAgent;
    private NavMeshPath _navMeshPath;

    private void Start()
    {
        _myNavMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshPath = new NavMeshPath();
    }

    private void Update()
    {
        _myNavMeshAgent.CalculatePath(targetVector3Data.value, _navMeshPath);
        if (_navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            _myNavMeshAgent.destination = targetVector3Data.value;
        }
        else
        {
            _myNavMeshAgent.destination = transform.position;
        }
    }
}
