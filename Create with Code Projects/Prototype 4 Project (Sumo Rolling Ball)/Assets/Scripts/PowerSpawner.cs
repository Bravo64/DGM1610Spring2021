using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawner : MonoBehaviour
{
    [SerializeField] 
    private float spawnInterval = 3.0f;
    [SerializeField]
    private Transform minXMarker;
    [SerializeField] 
    private Transform maxXMarker;
    [SerializeField] 
    private Transform minZMarker;
    [SerializeField] 
    private Transform maxZMarker;
    
    private List<Transform> _objectPool = new List<Transform>();
    private WaitForSeconds _waitForSecondsObj;

    void Start()
    {
        _waitForSecondsObj = new WaitForSeconds(spawnInterval);
        foreach (Transform child in transform)
        {
            _objectPool.Add(child);
        }

        StartCoroutine(SpawnEnemy());
    }
    
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            foreach (Transform enemy in _objectPool)
            {
                if (!enemy.gameObject.activeSelf)
                {
                    enemy.transform.position = GenerateSpawnPoint();
                    enemy.gameObject.SetActive(true);
                    break;
                }
            }
            yield return _waitForSecondsObj;
        }
    }
    
    Vector3 GenerateSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;
        spawnPoint.Set(Random.Range(minXMarker.position.x, maxXMarker.position.x),
            0,Random.Range(minZMarker.position.z, maxZMarker.position.z));
        return spawnPoint;
    }
}
