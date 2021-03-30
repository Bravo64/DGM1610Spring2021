using GameEvents;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBallController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float fallBoundary = -20.0f;
    [SerializeField]
    private VoidEvent checkEnemyCount;
    
    private Rigidbody _myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.position - transform.position).normalized;
        _myRigidbody.AddForce(lookDirection * speed);

        if (transform.position.y < fallBoundary)
        {
            checkEnemyCount.Raise();
            gameObject.SetActive(false);
        }
    }
}