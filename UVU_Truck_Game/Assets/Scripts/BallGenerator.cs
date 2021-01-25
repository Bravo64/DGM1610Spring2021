using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    private int number = 0;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (number < 25)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(ball, transform.position, transform.rotation);
            number++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
