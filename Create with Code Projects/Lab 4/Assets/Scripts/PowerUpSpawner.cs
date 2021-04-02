using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    private float minWaitTime = 1.0f;
    [SerializeField]
    private float maxWaitTime = 3.0f;
    [SerializeField] 
    private Transform minXMarker;
    [SerializeField] 
    private Transform maxXMarker;
    [SerializeField] 
    private Transform minZMarker;
    [SerializeField] 
    private Transform maxZMarker;

    private Vector3 spawnPosition;
    private List<GameObject> _powerUppPool = new List<GameObject>();
    private List<WaitForSeconds> _waitForSecondsObjs = new List<WaitForSeconds>();
    
    void Start()
    {
        foreach (Transform child in transform)
        {
            _powerUppPool.Add(child.gameObject);
        }

        for (int i = 0; i < 5; i++)
        {
            _waitForSecondsObjs.Add(new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime)));
        }
        
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            foreach (GameObject powerUp in _powerUppPool)
            {
                if (!powerUp.activeSelf)
                {
                    spawnPosition.Set(Random.Range(minXMarker.position.x,maxXMarker.position.x), 0, Random.Range(minZMarker.position.z,maxZMarker.position.z));
                    powerUp.transform.position = spawnPosition;
                    powerUp.SetActive(true);
                    break;
                }
            }
            yield return _waitForSecondsObjs[Random.Range(0, _waitForSecondsObjs.Count)];
        }
    }
}
