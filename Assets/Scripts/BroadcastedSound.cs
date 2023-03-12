using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastedSound
{
    public float duration = 0.5f;
    public float volume = 1f;
    public Vector3 origin;

    public BroadcastedSound(float duration, float volume, Vector3 origin)
    {
        this.duration = duration;
        this.volume = volume;
        this.origin = origin;
    }
}
