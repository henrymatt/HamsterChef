using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private CheekPouches _characterCheekPouches;
    [SerializeField] private CharacterHiding _characterHiding;
    [SerializeField] private CharacterMovement _characterMovement;

    [SerializeField] private DialogueSceneSO _firstIngredientPickupDialogue;
    private int _lifetimeIngredientsPickedUpCount = 0;

    private void OnEnable()
    {
        EventHandler.DidPickUpIngredientEvent += AccumulatePickedUpIngredients;
    }

    private void OnDisable()
    {
        EventHandler.DidPickUpIngredientEvent -= AccumulatePickedUpIngredients;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Item"));
    }
    public CheekPouches CharacterCheekPouches => _characterCheekPouches;
    public CharacterHiding CharacterHiding => _characterHiding;
    public CharacterMovement CharacterMovement => _characterMovement;
    
    // Game Milestones

    private void AccumulatePickedUpIngredients()
    {
        _lifetimeIngredientsPickedUpCount++;
        if (_lifetimeIngredientsPickedUpCount == 1)
        {
            EventHandler.CallShouldPresentDialogueEvent(_firstIngredientPickupDialogue, false);
        }
    }
    
    
}
