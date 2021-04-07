using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private float defaultMinInterval = 1.0f;
    [SerializeField]
    private float defaultMaxInterval = 3.0f;
    private List<GameObject> _objectPool = new List<GameObject>();
    private List<WaitForSeconds> _WFSObjs = new List<WaitForSeconds>();
    private GameObject _randomPick;

    void Start()
    {
        foreach (Transform child in transform)
        {
            _objectPool.Add(child.gameObject);
        }
        
        for (int i = 0; i < 5; i++)
        {
            _WFSObjs.Add(new WaitForSeconds(Random.Range(defaultMinInterval, defaultMaxInterval)));
        }
        
        StartCoroutine(SpawnObjects());
    }

    public void SetDifficultyLevel(int difficultyLevel)
    {
        defaultMinInterval /= difficultyLevel;
        defaultMaxInterval /= difficultyLevel;
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            for (int i = 0; i < 50; i++)
            {
                _randomPick = _objectPool[Random.Range(0, _objectPool.Count)];
                if (!_randomPick.activeSelf)
                {
                    _randomPick.SetActive(true);
                    break;
                }
            }
            yield return _WFSObjs[Random.Range(0, _WFSObjs.Count)];
        }
    }
}
