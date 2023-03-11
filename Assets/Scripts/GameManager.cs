using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private CheekPouches _characterCheekPouches;
    [SerializeField] private CharacterHiding _characterHiding;
    [SerializeField] private CharacterMovement _characterMovement;
    
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Item"));
    }
    public CheekPouches CharacterCheekPouches => _characterCheekPouches;
    public CharacterHiding CharacterHiding => _characterHiding;
    public CharacterMovement CharacterMovement => _characterMovement;
}
