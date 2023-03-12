using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    private AudioSource _nonDiageticAudioSource;
    [SerializeField] private AudioSource _creatureAudioSource;
    [SerializeField] private AudioSource _ambientAudioSource;
    
    [SerializeField] private AudioListener _playerListener;
    
    [SerializeField] private AudioClip _beingChasedClip;
    [SerializeField] private AudioClip _outdoorAmbianceClip;
    [SerializeField] private AudioClip[] _creatureChaseScreeches;

    private bool _didScreechOnceDuringThisChase = false;

    private void Start()
    {
        _nonDiageticAudioSource = GetComponent<AudioSource>();

        _ambientAudioSource.clip = _outdoorAmbianceClip;
        _ambientAudioSource.loop = true;
        _ambientAudioSource.Play();
    }

    private void OnEnable()
    {
        EventHandler.DidCreatureBeginChasingEvent += StartPlayingBeingChasedClip;
        EventHandler.DidCreatureBeginChasingEvent += PlayCreatureChaseScreech;
        EventHandler.DidCreatureStopChasingEvent += StopBeingChasedClip;
        EventHandler.DidHideEvent += StopBeingChasedClip;
    }

    private void OnDisable()
    {
        EventHandler.DidCreatureBeginChasingEvent -= StartPlayingBeingChasedClip;
        EventHandler.DidCreatureBeginChasingEvent -= PlayCreatureChaseScreech;
        EventHandler.DidCreatureStopChasingEvent -= StopBeingChasedClip;
        EventHandler.DidHideEvent -= StopBeingChasedClip;
    }

    private void PlayCreatureChaseScreech()
    {
        if (_creatureAudioSource.isPlaying || _didScreechOnceDuringThisChase) return;
        
        int randomClipIndex = Random.Range(0, _creatureChaseScreeches.Length + 1);
        Debug.Log("Random Index is: " + randomClipIndex);
        _creatureAudioSource.clip = _creatureChaseScreeches[randomClipIndex];
        _creatureAudioSource.loop = false;
        _creatureAudioSource.Play();
        _didScreechOnceDuringThisChase = true;
    }
    
    private void StartPlayingBeingChasedClip()
    {
        if (_nonDiageticAudioSource.clip == _beingChasedClip && _nonDiageticAudioSource.isPlaying) return;
        _nonDiageticAudioSource.clip = _beingChasedClip;
        _nonDiageticAudioSource.loop = true;
        StartCoroutine(FadeIn(_nonDiageticAudioSource, 1.0f));
    }

    private void StopBeingChasedClip()
    {
        StartCoroutine(FadeOut(_nonDiageticAudioSource, 2.0f));
        _didScreechOnceDuringThisChase = false;
    }

    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        
        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
    }
    
    
}