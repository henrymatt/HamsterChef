using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastedSound
{
    public string name = "";
    public float duration = 0.5f;
    public float volume = 1f;
    public Vector3 origin;

    public BroadcastedSound(string name, float duration, float volume, Vector3 origin)
    {
        this.name = name;
        this.duration = duration;
        this.volume = volume;
        this.origin = origin;
    }

    /// <summary>
    /// Gets the relative importance of the sound based on distance and volume
    /// </summary>
    /// <param name="listenerOrigin"></param>
    /// <returns></returns>
    public float GetSoundImportance(Vector3 listenerOrigin)
    {
        return Vector3.Distance(this.origin, listenerOrigin) * (1 / this.volume);
    }
}
