using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField]
    private Player playerScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerScript.isGrounded)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                playerScript.isGrounded = true;
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!playerScript.isGrounded)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                playerScript.isGrounded = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (playerScript.isGrounded)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                playerScript.isGrounded = false;
            }
        }
    }
}