using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBroadcast : SingletonMonoBehaviour<AudioBroadcast>
{
    private List<BroadcastedSound> broadcastedSounds = new List<BroadcastedSound>();
    private List<BroadcastedSound> broadcastedSoundsToBeRemoved = new List<BroadcastedSound>();

    private List<AudioBroadcastListener> listeners = new List<AudioBroadcastListener>();

    private void Update()
    {
        foreach (BroadcastedSound sound in broadcastedSounds)
        {
            sound.duration -= Time.deltaTime;
            if (sound.duration <= 0f)
                broadcastedSoundsToBeRemoved.Add(sound);
        }
        foreach (BroadcastedSound soundToBeRemoved in broadcastedSoundsToBeRemoved)
        {
            broadcastedSounds.Remove(soundToBeRemoved);
        }
        foreach (AudioBroadcastListener listener in listeners)
        {
            listener.sounds = broadcastedSounds;
        }
    }

    public void AddBroadcastedSound(BroadcastedSound sound)
    {
        //Check for if sound with the same name already exists in list, if it does replace it instead of creating a new instance
        for (int i = 0; i < broadcastedSounds.Count; i++)
        {
            if (broadcastedSounds[i].name == sound.name)
            {
                broadcastedSounds[i] = sound;
                return;
            }
        }
        broadcastedSounds.Add(sound);
    }

    public void AddListener(AudioBroadcastListener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(AudioBroadcastListener listener)
    {
        listeners.Remove(listener);
    }
}
