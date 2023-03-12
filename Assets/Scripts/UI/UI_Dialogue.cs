using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Dialogue : MonoBehaviour
{
    [SerializeField] private float _charSpeed = 0.2f;
    [SerializeField] private float _charSpeedFast = 0.05f;
    
    [SerializeField] private Image _visibleDialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private Image _dialoguePortrait;
    [SerializeField] private Image _continueAffordance;

    private bool _isDisplayingDialogue = false;
    private bool _dialogueCausingPause = false;
    private bool _canAdvanceDialogue = false;
    private Queue<DialogueSceneSO> dialogueQueue = new Queue<DialogueSceneSO>();

    private void OnEnable()
    {
        EventHandler.ShouldPresentDialogueEvent += StartDisplayingDialogueScene;
    }

    private void OnDisable()
    {
        EventHandler.ShouldPresentDialogueEvent -= StartDisplayingDialogueScene;
    }
    
    private void Update()
    {
        CheckForDialogueAdvance();
    }

    // TODO: Allow scenes of multiple pages
    private void StartDisplayingDialogueScene(DialogueSceneSO dialogueSceneSo, bool shouldPause)
    {
        // Always stop A button from being used for other things?
        if (_isDisplayingDialogue)
        {
            // TODO: Fix bug when enqueuing a new dialogue scene
            if (dialogueQueue.Contains(dialogueSceneSo)) return;
            dialogueQueue.Enqueue(dialogueSceneSo);
            return;
        }

        _continueAffordance.gameObject.SetActive(false);
        _canAdvanceDialogue = false;
        
        // display dialogue scene
        _dialoguePortrait.sprite = dialogueSceneSo.speakingCharacter.characterPortrait;
        _visibleDialoguePanel.gameObject.SetActive(true);
        
        // begin animating text
        StartCoroutine(AnimateTextOn(dialogueSceneSo.dialogue, dialogueSceneSo.speakingCharacter));
    }

    private void CheckForDialogueAdvance()
    {
        // if dialogue being presented, press A to proceed
        // while dialogue is being spelled out, hold A to speed up
        // but don't auto advance if A is being held down
        if (Input.GetButtonDown("Fire1") && _canAdvanceDialogue)
        {
            AdvanceDialogue();
        }
    }

    private void AdvanceDialogue()
    {
        if (dialogueQueue.Count > 0)
        {
            StartDisplayingDialogueScene(dialogueQueue.Dequeue(), false);
            return;
        }
        
        _visibleDialoguePanel.gameObject.SetActive(false);
    }

    IEnumerator AnimateTextOn(string dialogueToAnimate, DialogueCharacterSO dialogueCharacterSo)
    {
        for (int i = 0; i < dialogueToAnimate.Length; i++)
        {
            _dialogueText.text = dialogueToAnimate.Substring(0, i);
            float timeToWait = Input.GetButton("Fire1") ? _charSpeedFast : _charSpeed;
            if (i % 3 == 0) AudioManager.Instance.PlayCharacterVoice(dialogueCharacterSo.characterVoice);
            yield return new WaitForSeconds(timeToWait);
        }

        _dialogueText.text = dialogueToAnimate;
        _canAdvanceDialogue = true;
        _continueAffordance.gameObject.SetActive(true);
    }

}
