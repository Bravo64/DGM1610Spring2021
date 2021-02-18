using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] 
    private GameObject exlposionParticle;
    
    [SerializeField] 
    private GameObject buildingDecorations;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vehicle"))
        {
            exlposionParticle.SetActive(true);
            buildingDecorations.SetActive(false);
        }
    }
}
