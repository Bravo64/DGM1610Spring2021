using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CheckAudioPlayingBehaviour : MonoBehaviour
{
    private AudioSource _myAudioSource;
    
    void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();
    }

    public void PlayIfNotPlaying()
    {
        if (!_myAudioSource.isPlaying)
        {
            _myAudioSource.Play();
        }
    }
}
