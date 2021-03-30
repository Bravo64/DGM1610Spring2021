using System;
using System.Collections;
using GameEvents;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerBall : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private Transform focalPoint;
    [SerializeField]
    private float powerUpDuration = 5.0f;
    [SerializeField]
    private float powerUpStrength = 15.0f;
    [SerializeField]
    private Transform powerUpRing;
    [SerializeField]
    private float fallBoundary = -20.0f;
    [SerializeField]
    private VoidEvent reloadLevel;
    private Rigidbody _myRigidbody;
    private bool _powerUp1Active = false;

    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            _myRigidbody.angularVelocity = _myRigidbody.angularVelocity / 4;
        }

        float verticalInput = Input.GetAxis("Vertical");
        _myRigidbody.AddForce(focalPoint.forward * speed * verticalInput);

        if (_powerUp1Active)
        {
            powerUpRing.position = transform.position;
        }
        
        if (transform.position.y < fallBoundary)
        {
            reloadLevel.Raise();
        }
    }

    public void PowerUp1Collected()
    {
        StartCoroutine(ActivatePowerUp1());
    }

    IEnumerator ActivatePowerUp1()
    {
        powerUpRing.gameObject.SetActive(true);
        _powerUp1Active = true;
        
        yield return new WaitForSeconds(powerUpDuration);
        
        powerUpRing.gameObject.SetActive(false);
        _powerUp1Active = false;
        StopCoroutine(ActivatePowerUp1());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_powerUp1Active)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = (other.transform.position - transform.position);

                Debug.Log("Player collided with " + other.gameObject +
                          " with PowerUp 1 set to " + _powerUp1Active);
                enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            }
        }
    }
}
