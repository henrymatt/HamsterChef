using System;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    private AudioSource _sfxAudioSource;
    [SerializeField] private AudioClip _beingChasedClip;

    private void Start()
    {
        _sfxAudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventHandler.DidCreatureBeginChasingEvent += StartPlayingBeingChasedClip;
        EventHandler.DidCreatureStopChasingEvent += StopBeingChasedClip;
        EventHandler.DidHideEvent += StopBeingChasedClip;
    }

    private void OnDisable()
    {
        EventHandler.DidCreatureBeginChasingEvent -= StartPlayingBeingChasedClip;
        EventHandler.DidCreatureStopChasingEvent -= StopBeingChasedClip;
        EventHandler.DidHideEvent -= StopBeingChasedClip;
    }

    private void StartPlayingBeingChasedClip()
    {
        if (_sfxAudioSource.clip == _beingChasedClip && _sfxAudioSource.isPlaying) return;
        _sfxAudioSource.clip = _beingChasedClip;
        _sfxAudioSource.loop = true;
        _sfxAudioSource.Play();
    }

    private void StopBeingChasedClip()
    {
        _sfxAudioSource.Stop();
    }
}