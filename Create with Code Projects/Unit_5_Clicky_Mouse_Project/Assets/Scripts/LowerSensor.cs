using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerSensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}
