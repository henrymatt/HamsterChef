using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBroadcastListener : MonoBehaviour
{
    public List<BroadcastedSound> sounds = new List<BroadcastedSound>();
    public float hearingDistance = 90f;
    [Tooltip("How much louder a sound has to be relative to the current closest sound for the listener to switch to that sound")]
    private BroadcastedSound closestSound = null;
    // Start is called before the first frame update
    void Start()
    {
        AudioBroadcast.Instance.AddListener(this);
    }

    private void FixedUpdate()
    {
        if (sounds.Count > 0)
        {
            closestSound = null;
            float closestSoundDistance = 0f;
            foreach (BroadcastedSound sound in sounds)
            {
                float soundDistance = sound.GetSoundImportance(transform.position);
                if (soundDistance <= hearingDistance)
                {
                    if (closestSound == null)
                    {
                        closestSound = sound;
                        closestSoundDistance = soundDistance;
                    }
                    else
                    {
                        if (soundDistance < closestSoundDistance)
                        {
                            closestSound = sound;
                            closestSoundDistance = soundDistance;
                        }
                    }
                }
            }
        }
    }

    public BroadcastedSound GetClosestSound()
    {
        if (sounds.Count > 0)
            return closestSound;
        else
            return null;
    }
}
