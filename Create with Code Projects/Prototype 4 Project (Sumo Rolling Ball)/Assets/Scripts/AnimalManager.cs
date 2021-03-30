using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    [SerializeField]
    private float animalSpawnInterval = 1.0f;
    [SerializeField] 
    private Transform minXMarker, maxXMarker, maxZMarker;
    
    private WaitForSeconds _waitForSecondsObj;
    private int _animalIndex;
    private List<Transform> _animalObjectPool = new List<Transform>();
    private Vector3 _firePosition;

    IEnumerator Start()
    {
        foreach (Transform child in transform)
        {
            _animalObjectPool.Add(child);
        }
        _waitForSecondsObj = new WaitForSeconds(animalSpawnInterval);
        
        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                _animalIndex = Random.Range(0, _animalObjectPool.Count);
                if (!_animalObjectPool[_animalIndex].gameObject.activeSelf)
                {
                    _firePosition.Set(Random.Range(minXMarker.position.x, maxXMarker.position.x),
                                      0.5f, maxZMarker.position.z);
                    _animalObjectPool[_animalIndex].transform.position = _firePosition;
                    _animalObjectPool[_animalIndex].gameObject.SetActive(true);
                    break;
                }
            }
            
            yield return _waitForSecondsObj;
        }
    }
}
