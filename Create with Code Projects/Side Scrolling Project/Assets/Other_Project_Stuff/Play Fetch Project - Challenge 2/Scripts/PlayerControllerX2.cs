using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX2 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] dogsPool;

    [SerializeField] private float spawnEnablerInterval = 1.5f;
    private bool _spawnEnabled = true;
    private WaitForSeconds _waitForSecondsObj;

    private void Start()
    {
        _waitForSecondsObj = new WaitForSeconds(spawnEnablerInterval);
    }

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && _spawnEnabled)
        {
            foreach (var dog in dogsPool)
            {
                if (!dog.activeSelf)
                {
                    dog.transform.position = transform.position;
                    dog.SetActive(true);
                    _spawnEnabled = false;
                    StartCoroutine(SpawnEnabler());
                    break;
                }
            }
        }
    }

    IEnumerator SpawnEnabler()
    {
        yield return _waitForSecondsObj;
        _spawnEnabled = true;
    }
}
