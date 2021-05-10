using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMoveBehaviour : MonoBehaviour
{
    public enum Directions { X, Y, Z }
    public enum DirectionTypes { Global, Local}
    
    public float speed;
    public Directions alongAxis = Directions.X;
    public DirectionTypes directionType = DirectionTypes.Global;
    
    private CharacterController _myCharacterController;
    private Vector3 _globalDirection;
    
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();

        if (directionType == DirectionTypes.Global)
        {
            switch (alongAxis)
            {
                case Directions.X:
                    _globalDirection = Vector3.right;
                    break;
                case Directions.Y:
                    _globalDirection = Vector3.up;
                    break;
                case Directions.Z:
                    _globalDirection = Vector3.forward;
                    break;
                default:
                    _globalDirection = Vector3.right;
                    break;
            }
            StartCoroutine(ConstMoveGlobal());
        }
        else if (directionType == DirectionTypes.Local)
        {
            switch (alongAxis)
            {
                case Directions.X:
                    StartCoroutine(ConstMoveX());
                    break;
                case Directions.Y:
                    StartCoroutine(ConstMoveY());
                    break;
                case Directions.Z:
                    StartCoroutine(ConstMoveZ());
                    break;
                default:
                    StartCoroutine(ConstMoveX());
                    break;
            }
        }
    }
    

    public void SetSpeedValue(float inputSpeed)
    {
        speed = inputSpeed;
    }

    IEnumerator ConstMoveGlobal()
    {
        while (true)
        {
            _myCharacterController.Move(_globalDirection * (speed * Time.deltaTime));
            yield return 0;
        }
    }
    
    IEnumerator ConstMoveX()
    {
        while (true)
        {
            _myCharacterController.Move(transform.right * (speed * Time.deltaTime));
            yield return 0;
        }
    }
    
    IEnumerator ConstMoveY()
    {
        while (true)
        {
            _myCharacterController.Move(transform.up * (speed * Time.deltaTime));
            yield return 0;
        }
    }
    
    IEnumerator ConstMoveZ()
    {
        while (true)
        {
            _myCharacterController.Move(transform.forward * (speed * Time.deltaTime));
            yield return 0;
        }
    }
}
