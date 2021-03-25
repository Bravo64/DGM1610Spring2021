using System;
using System.Collections;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    [SerializeField] 
    private VoidEvent playerDead;
    private void OnTriggerEnter(Collider other)
    {
        playerDead.Raise();
    }
}
