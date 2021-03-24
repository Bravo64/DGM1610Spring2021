using System.Collections;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce, groundBounceForce;
    public VoidEvent reloadSceneEvent;
    private float gravity = -4.0f;
    private Rigidbody playerRb;
    private MeshRenderer playerRenderer;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 currentGravity = Physics.gravity;
        currentGravity.y = gravity;
        Physics.gravity = currentGravity;
        playerRb = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<MeshRenderer>();
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 2, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(Dead());
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            other.gameObject.SetActive(false);

        }
        
        else if (other.gameObject.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up * groundBounceForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(bounceSound);
        }

    }

    IEnumerator Dead()
    {
        playerRenderer.enabled = false;
        playerRb.constraints = RigidbodyConstraints.FreezeAll;
        explosionParticle.Play();
        playerAudio.PlayOneShot(explodeSound, 1.0f);
        gameOver = true;
        Debug.Log("Game Over!");
        yield return new WaitForSeconds(3.0f);
        reloadSceneEvent.Raise();
    }

}
