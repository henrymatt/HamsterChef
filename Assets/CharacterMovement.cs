using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private float _moveSpeed;
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        float movePositionX = hInput * _moveSpeed * Time.deltaTime;
        float movePositionZ = vInput * _moveSpeed * Time.deltaTime;
        
        Vector3 movePosition = new Vector3(movePositionX, 0f, movePositionZ);
        
        _characterController.Move(movePosition);
    }
}
