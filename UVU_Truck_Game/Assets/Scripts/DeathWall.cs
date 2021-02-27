using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    [SerializeField]
    private float currentSpeed = 0.0f;
    [SerializeField]
    private float speedIncreaseFactor = 1.0f;

    // Scriptable Object with the player's location.
    [SerializeField] private FloatData playerYLocationObj;

    // Update is called once per frame
    void Update()
    {
        // We want to match the player on the Y axis.
        Vector3 myPosition = transform.position;
        myPosition.y = playerYLocationObj.value;
        transform.position = myPosition;
        transform.Translate(Vector3.right * (currentSpeed * Time.deltaTime));
        currentSpeed += Time.deltaTime * speedIncreaseFactor;
    }
}
