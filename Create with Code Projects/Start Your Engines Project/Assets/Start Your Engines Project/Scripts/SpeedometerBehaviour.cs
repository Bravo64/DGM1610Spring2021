using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SpeedometerBehaviour : MonoBehaviour
{
    [SerializeField] 
    private Transform player;
    
    private TextMeshProUGUI _speedometerLabel;
    private int _mphSpeed;
    private Vector3 posLastFrame;
    private WaitForSeconds waitForSecondsObj;

    private void Start()
    {
        _speedometerLabel = GetComponent<TextMeshProUGUI>();
        waitForSecondsObj = new WaitForSeconds(0.1f);
        StartCoroutine(UpdateSpeedometer());
    }

    private void LateUpdate()
    {
        _mphSpeed = (int)Mathf.Round((Vector3.Distance (posLastFrame, player.position) / 0.02f) * 3.5f);
        posLastFrame = player.position;
    }

    IEnumerator UpdateSpeedometer()
    {
        while (true)
        {
            _speedometerLabel.SetText("Speed: " + _mphSpeed + "mph");
            yield return waitForSecondsObj;
        }
    }
}
