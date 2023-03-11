using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private Camera _camera;
    [SerializeField] private GameObject _characterMesh;
    private Animator _animator;

    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _rotateSpeed = 1f;
    [SerializeField] private float _walkSpeedPercentage = 0.3f;

    // Animator fields
    private float _lastMoveMagnitude;
    private float _currentMoveMagnitude;
    [SerializeField] private float _moveAnimationTransitionSpeed = 8f;
    private bool _canMove = true;


    public void DisableMovement() => _canMove = false;
    public void EnableMovement() => _canMove = true;

    public bool CanMove() => _canMove;
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
        if (_characterMesh != null)
        {
            _animator = _characterMesh.GetComponentInChildren<Animator>();
        }
    }

    private void Update()
    {
        if (_canMove)
        {
            HandleMovement();
        }
    }

    private void LateUpdate()
    {
        UpdateAnimator();
    }

    private void HandleMovement()
    {
        float maxSpeed = Input.GetButton("Fire3") ? _walkSpeedPercentage : 1.0f;
        
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        bool isInput = (hInput != 0 || vInput != 0);

        Vector3 moveDirection = ((transform.forward * vInput) + (transform.right * hInput));
        if (moveDirection.magnitude > maxSpeed) moveDirection = moveDirection.normalized * maxSpeed;
        moveDirection *= _moveSpeed;
        Vector3 movePosition = new Vector3(moveDirection.x, 0f, moveDirection.z);
        
        _characterController.Move(movePosition * Time.deltaTime);

        // Store current movement magnitude to send to animator
        _currentMoveMagnitude = movePosition.magnitude;

        if (!isInput) return;
        transform.rotation = Quaternion.Euler(0f, _camera.transform.rotation.eulerAngles.y, 0f);
        Quaternion newRotation = Quaternion.LookRotation(movePosition);
        _characterMesh.transform.rotation = Quaternion.Slerp(_characterMesh.transform.rotation, newRotation, _rotateSpeed * Time.deltaTime);
    }

    private void UpdateAnimator() {
        if (_animator == null) return;
        _currentMoveMagnitude = Mathf.MoveTowards(_lastMoveMagnitude, _currentMoveMagnitude, _moveAnimationTransitionSpeed * Time.deltaTime);
        _animator.SetFloat("MoveMagnitude", _currentMoveMagnitude);
        _lastMoveMagnitude = _currentMoveMagnitude;
    }

    public Vector3 GetCharacterForwardVector() =>  _characterMesh.transform.forward;

}
