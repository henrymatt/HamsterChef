using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private Camera _camera;
    [SerializeField] private GameObject _characterMesh;

    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _rotateSpeed = 1f;
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        bool isInput = (hInput != 0 || vInput != 0);

        Vector3 moveDirection = ((transform.forward * vInput) + (transform.right * hInput)).normalized;
        moveDirection *= _moveSpeed;
        Vector3 movePosition = new Vector3(moveDirection.x, 0f, moveDirection.z);
        
        _characterController.Move(movePosition * Time.deltaTime);

        if (!isInput) return;
        transform.rotation = Quaternion.Euler(0f, _camera.transform.rotation.eulerAngles.y, 0f);
        Quaternion newRotation = Quaternion.LookRotation(movePosition);
        _characterMesh.transform.rotation = Quaternion.Slerp(_characterMesh.transform.rotation, newRotation, _rotateSpeed * Time.deltaTime);
    }
}
