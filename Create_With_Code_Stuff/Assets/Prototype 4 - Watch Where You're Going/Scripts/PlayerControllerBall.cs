using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerBall : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private Transform focalPoint;
    private Rigidbody _myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            _myRigidbody.angularVelocity = _myRigidbody.angularVelocity / 4;
        }

        float verticalInput = Input.GetAxis("Vertical");
        _myRigidbody.AddForce(focalPoint.forward * speed * verticalInput);
    }
}
