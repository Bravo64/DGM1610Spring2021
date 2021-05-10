using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class CharacterImpactBehaviour : MonoBehaviour
{
    public float velocityThreshold = 1.0f;
    public UnityEvent onImpactEvent;
    
    private CharacterController _myCharacterController;
    private float _savedVelocity;
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        if (_myCharacterController.velocity.magnitude < _savedVelocity - velocityThreshold)
        {
            onImpactEvent.Invoke();
        }
        _savedVelocity = _myCharacterController.velocity.magnitude;
    }
}
