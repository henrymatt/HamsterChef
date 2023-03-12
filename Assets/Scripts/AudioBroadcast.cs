using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBroadcast : SingletonMonoBehaviour<AudioBroadcast>
{

    public List<BroadcastedSound> broadcastedSounds = new List<BroadcastedSound>();
    private List<BroadcastedSound> stillActivebroadcastedSounds = new List<BroadcastedSound>();
    private void Update()
    {
        foreach (BroadcastedSound activeSound in stillActivebroadcastedSounds)
        {
            broadcastedSounds.Add(activeSound);
        }
        stillActivebroadcastedSounds.Clear();
        for(int i = 0; i < broadcastedSounds.Count; i++ )
        {
            broadcastedSounds[i].duration -= Time.deltaTime;
            if (broadcastedSounds[i].duration > 0f)
                stillActivebroadcastedSounds.Add(broadcastedSounds[i]);
        }
        broadcastedSounds.Clear();
    }

    public void AddBroadcastedSound(BroadcastedSound sound)
    {
        broadcastedSounds.Add(sound);
    }
}
