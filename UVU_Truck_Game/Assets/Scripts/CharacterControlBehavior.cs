using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControlBehavior : MonoBehaviour
{
    public float speed = 3f, gravity = -20f, jumpForce = 100f;

    private CharacterController controller;
    private Vector3 movement, rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.Set(speed*Input.GetAxis("Vertical"), gravity, 0);
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            movement.y += jumpForce;
        }
        rotation.y = Input.GetAxis("Horizontal");
        transform.Rotate(rotation);
        movement = transform.TransformDirection(movement);
        controller.Move(movement * Time.deltaTime);
    }
}
