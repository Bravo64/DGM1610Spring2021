using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDisableOther2D : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
    }
}
