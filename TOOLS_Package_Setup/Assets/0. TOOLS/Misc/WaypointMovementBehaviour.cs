using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaypointMovementBehaviour : MonoBehaviour
{
    public enum CycleType { PingPong, Loop}
    public enum Modes { UseWaypointList, UseActiveChildren}
    
    public Transform itemToMove;
    public float movementSpeed, speedRandomizeAmount, waypointMinDistance;
    public Modes mode = Modes.UseWaypointList;
    public CycleType movementCycleType = CycleType.PingPong;
    
    public  List<Transform> waypointList = new List<Transform>();
    
    private int _movementDirection = 1;
    private int i = 0;
    
    void Start()
    {
        if (speedRandomizeAmount != 0.0f)
        {
            movementSpeed += Random.Range(-speedRandomizeAmount, speedRandomizeAmount);
        }

        if (mode == Modes.UseActiveChildren)
        {
            waypointList.Clear();
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf)
                {
                    waypointList.Add(child);
                }
            }
        }
    }
    
    void Update()
    {
        itemToMove.position = Vector3.MoveTowards(itemToMove.position, waypointList[i].position,
            movementSpeed * Time.deltaTime);
        
        if (Vector3.Distance(itemToMove.position, waypointList[i].position) < waypointMinDistance)
        {
            if (i >= waypointList.Count - 1)
            {
                i = waypointList.Count - 1;
                if (movementCycleType == CycleType.PingPong)
                {
                    _movementDirection = -1;
                }
                else
                {
                    i = -1;
                }
            }
            else if (i <= 0 && movementCycleType == CycleType.PingPong)
            {
                i = 0;
                _movementDirection = 1;
            }
            i += _movementDirection;
        }
    }
}