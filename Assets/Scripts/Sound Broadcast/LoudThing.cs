using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoudThing : MonoBehaviour
{
    public float volume;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            timer = 1f;
            AudioBroadcast.Instance.AddBroadcastedSound(new BroadcastedSound("Loud boi", volume, 0.1f, transform.position));
        }
    }
}
