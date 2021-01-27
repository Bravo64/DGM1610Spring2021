using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private int creationLimit = 200;
    [SerializeField] private int ballsPerSecond = 2;

    // Start Function is called before the first frame update
    // (or at the gameObject's creation/reactivation)
    IEnumerator Start()
    {
        // The number of balls we created within this scene
        int ballsTotal = 0;
        // loop until we reach the limit value
        while (ballsTotal < creationLimit)
        {
            // Wait to create the next ball
            // The ballsPerSecond variable determines the wait time
            yield return new WaitForSeconds(1.0f / ballsPerSecond);
            // Create the ball at our exact location
            Instantiate(ball, transform.position, transform.rotation);
            // Count this ball
            ballsTotal++;
        }
    }
}
