using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControlBehavior : MonoBehaviour
{
    public float speed = 3f, setGravity = -20f, jumpForce = 100f;

    private float currentGravity = -20f;
    private CharacterController controller;
    private Vector3 movement, rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentGravity = setGravity;
    }

    // Update is called once per frame
    void Update()
    {
        movement.Set(speed*Input.GetAxis("Vertical"), currentGravity, 0);
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            currentGravity = 0;
            movement.y += jumpForce;
        }
        if (currentGravity > setGravity)
        {
            currentGravity += setGravity * Time.deltaTime;
        }
        rotation.y = Input.GetAxis("Horizontal");
        transform.Rotate(rotation);
        movement = transform.TransformDirection(movement);
        controller.Move(movement * Time.deltaTime);
    }
}
