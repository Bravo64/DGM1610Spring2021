using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] musicSegments;

    private AudioSource _myAudioSource;
    private List<WaitForSeconds> musicLengthWaitForSeconds = new List<WaitForSeconds>();
    private int _currentSong;
    
    void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();
        foreach (var segment in musicSegments)
        {
            musicLengthWaitForSeconds.Add(new WaitForSeconds(segment.length));
        }
        StartCoroutine(PlayNewSong());
    }

    private IEnumerator PlayNewSong()
    {
        while (true)
        {
            _currentSong = Random.Range(0, musicSegments.Length);
            _myAudioSource.PlayOneShot(musicSegments[_currentSong]);
            yield return musicLengthWaitForSeconds[_currentSong];
        }
    }
}
