using System;
using System.Collections;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;

public class CureCrate : MonoBehaviour
{
    [SerializeField]
    private IntData carriedCrates;
    [SerializeField]
    private IntData livingCrates;
    [SerializeField]
    private VoidEvent updateCrateText;
    [SerializeField]
    private GameObject blueInside;
    [SerializeField]
    private GameObject blueTrail;
    [SerializeField]
    private GameObject smashParticleObject;
    private bool _dead = false;

    private void Start()
    {
        livingCrates.value += 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_dead)
        {
            return;
        }

        if (other.CompareTag("Cargo"))
        {
            carriedCrates.value += 1;
            updateCrateText.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_dead)
        {
            return;
        }

        if (other.CompareTag("Cargo"))
        {
            carriedCrates.value -= 1;
            updateCrateText.Raise();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_dead)
        {
            return;
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            livingCrates.value -= 1;
            gameObject.tag = "Ground";
            blueInside.SetActive(false);
            blueTrail.SetActive(false);
            smashParticleObject.SetActive(true);
            _dead = true;
            this.enabled = false;
        }
        
        if (livingCrates.value < carriedCrates.value)
        {
            carriedCrates.value = livingCrates.value;
            updateCrateText.Raise();
        }
    }
}
