using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using GameEvents;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 10.0f;
    [SerializeField]
    private AudioClip jumpSound;
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private SkinnedMeshRenderer myMeshRenderer;
    [SerializeField]
    private Rigidbody deadPlayerRigidbody;
    [SerializeField]
    private ParticleSystem runParticle;
    [SerializeField]
    private ParticleSystem jumpParticle;
    [SerializeField]
    private ParticleSystem deathParticle;
    [SerializeField]
    private VoidEvent reloadScene;
    
    private Rigidbody _myRigidbody;
    private Animator _myAnimator;
    private AudioSource _myAudioSource;
    private bool _isGrounded = false;
    private bool _deathSetupComplete = false;
    
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _myAnimator = GetComponent<Animator>();
        _myAudioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _myRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpParticle.Play();
            runParticle.Stop();
            _isGrounded = false;
            if (!_myAudioSource.isPlaying)
            {
                _myAudioSource.pitch = Random.Range(0.5f, 0.75f);
                _myAudioSource.PlayOneShot(jumpSound);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        _isGrounded = true;
        _myAnimator.SetBool("Grounded", true);
        runParticle.Play();
    }
    
    private void OnCollisionExit(Collision other)
    {
        _isGrounded = false;
        _myAnimator.SetBool("Grounded", false);
    }

    public void PlayerDead()
    {
        StartCoroutine(GameOver());
    }
    
    IEnumerator GameOver()
    {
        if (!_deathSetupComplete)
        {
            myMeshRenderer.enabled = false;
            _myAudioSource.pitch = 1.0f;
            _myAudioSource.PlayOneShot(deathSound);
            deadPlayerRigidbody.gameObject.SetActive(true);
            deadPlayerRigidbody.AddTorque(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
            deadPlayerRigidbody.AddForce((transform.up - transform.forward) * 9.0f);
            deathParticle.Play();
            _deathSetupComplete = true;
            yield return new WaitForSeconds(2.0f);
            reloadScene.Raise();
        }
    }
}
