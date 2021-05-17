using System.Collections;
using UnityEngine;

public class CharacterPushPullBehaviour : MonoBehaviour
{
    public enum ControllerLocations { UseMyCharacterController, UseParentCharacterController }
    public enum ForceType { PullTowardsTarget, PushAwayFromTarget }
    public enum Modes { ConstantMove, MoveOnTriggerStay }

    public ControllerLocations controller = ControllerLocations.UseMyCharacterController;
    public float movementSpeed;
    public Transform targetObject;
    public ForceType forceType = ForceType.PullTowardsTarget;
    public Modes mode = Modes.ConstantMove;
    public bool triggerOtherBecomesTarget = false;
    public bool beginOnStart = true;

    private CharacterController _activeController;
    private Vector3 _direction;
    private float _speedActual;

    void Start()
    {
        if (controller == ControllerLocations.UseMyCharacterController)
        {
            _activeController = GetComponent<CharacterController>();
        }
        else
        {
            _activeController = transform.parent.GetComponent<CharacterController>();
        }

        if (forceType == ForceType.PullTowardsTarget)
        {
            _speedActual = movementSpeed;
        }
        else
        {
            _speedActual = -movementSpeed;
        }
        
        if (beginOnStart && mode == Modes.ConstantMove)
        {
            ActivateConstantMove();
        }
    }

    public void TogglePushPull()
    {
        _speedActual = -_speedActual;

        if (forceType == ForceType.PullTowardsTarget)
        {
            forceType = ForceType.PushAwayFromTarget;
        }
        else
        {
            forceType = ForceType.PullTowardsTarget;
        }
    }
    
    public void MoveOneFrame()
    {
        _direction = (targetObject.transform.position - transform.position).normalized;
        _activeController.Move(_direction * (_speedActual * Time.deltaTime));
    }
    
    public void ActivateConstantMove()
    {
        StartCoroutine(ConstantMove());
    }

    IEnumerator ConstantMove()
    {
        while (true)
        {
            MoveOneFrame();
            yield return 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerOtherBecomesTarget)
        {
            targetObject = other.transform;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        MoveOneFrame();
    }

    public void PauseConstantMove()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        PauseConstantMove();
    }
}
