using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocalPoint : MonoBehaviour
{
    [SerializeField]
    private float rotationSensitivity = 45.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            transform.RotateAround(transform.position, transform.up, -Input.GetAxis("Horizontal") * rotationSensitivity * Time.deltaTime);
        }
    }
}
