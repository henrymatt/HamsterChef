using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerVolume : MonoBehaviour
{
    [SerializeField] private DialogueSceneSO _dialogueToPlay;
    private Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            EventHandler.CallShouldPresentDialogueEvent(_dialogueToPlay, false);
            Destroy(gameObject);
        }
    }
}
