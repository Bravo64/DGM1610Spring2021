using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyX : MonoBehaviour
{
    public float speed;
    public GameObject playerGoal;
    private Rigidbody enemyRb;
    private SpawnManagerX spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        spawnManager = transform.parent.GetComponent<SpawnManagerX>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * (speed * Time.deltaTime));

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, disable it
        if (other.gameObject.name == "Enemy Goal" || 
            other.gameObject.name == "Player Goal")
        {
            spawnManager.enemyCount--;
            spawnManager.CheckEnemyCount();
            gameObject.SetActive(false);
        }
    }
}
