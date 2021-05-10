using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaypointMovementBehaviour : MonoBehaviour
{
    public enum PathType { PingPong, Loop, Randomize}
    public enum Modes { UseWaypointList, UseActiveChildren}
    
    public Transform itemToMove;
    public float movementSpeed, speedRandomizeAmount;
    public Modes mode = Modes.UseWaypointList;
    public PathType movementPathType = PathType.PingPong;
    
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
        
        if (Vector3.Distance(itemToMove.position, waypointList[i].position) < 0.01f)
        {
            if (i >= waypointList.Count - 1)
            {
                i = waypointList.Count - 1;

                switch (movementPathType)
                {
                    case PathType.PingPong:
                        _movementDirection = -1;
                        break;
                    case PathType.Loop:
                        i = -1;
                        break;
                    case PathType.Randomize:
                        i = Random.Range(0, waypointList.Count) - 1;
                        break;
                }
            }
            else if (i <= 0 && movementPathType == PathType.PingPong)
            {
                i = 0;
                _movementDirection = 1;
            }
            i += _movementDirection;
        }
    }
}