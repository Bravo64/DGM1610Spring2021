using System;
using System.Collections;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    [Header("----------- VALUE VARIABLES -----------", order = 0)] [Space(10, order = 1)]
    
    [SerializeField]
    private float currentSpeed = 0.0f;
    [SerializeField]
    private float speedIncreaseFactor = 1.0f;

    [Header("----------- SCRIPTABLE OBJECTS -----------", order = 2)] [Space(10, order = 3)]
    
    // Scriptable Object with the player's location.
    [SerializeField] private FloatData playerYLocationObj;

    [Header("----------- EVENTS -----------", order = 4)] [Space(10, order = 5)]
    
    // Scriptable Object Event letting scene loader know to reset level.
    [SerializeField] private VoidEvent restartLevelEvent;
    
    // Update is called once per frame
    void Update()
    {
        // We want to match the player on the Y axis.
        Vector3 myPosition = transform.position;
        myPosition.y = playerYLocationObj.value;
        transform.position = myPosition;
        transform.Translate(Vector3.right * (currentSpeed * Time.deltaTime));
        currentSpeed += Time.deltaTime * speedIncreaseFactor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vehicle"))
        {
            restartLevelEvent.Raise();
        }
    }
}
