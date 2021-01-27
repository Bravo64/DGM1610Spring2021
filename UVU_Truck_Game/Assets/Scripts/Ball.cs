using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform highlight;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = quaternion.identity;
    }
}
