using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(ball);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
