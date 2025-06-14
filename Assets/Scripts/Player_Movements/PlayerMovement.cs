using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InputController))]
public class PlayerMovement : MonoBehaviour
{
    private InputController _inputController;
    private CharacterController _characterController;
    
    [Header("Movement Settings")]
    [SerializeField] private MovementConfig _movementConfig;
    
    private Vector2 _moveInput;
    private float _verticalVelocity;
    private float _gravity = -9.81f;

    private void Awake()
    {
        _inputController = GetComponent<InputController>();
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        if (_inputController != null)
        {
            _inputController.MoveEvent += HandleMoveInput;
            _inputController.JumpEvent += Jump;
            _inputController.JumpUpEventCanceled += CancelJump;
        }
    }
    
    private void OnDisable()
    {
        if (_inputController != null)
        {
            _inputController.MoveEvent -= HandleMoveInput;
            _inputController.JumpEvent -= Jump;
            _inputController.JumpUpEventCanceled -= CancelJump;
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMoveInput(Vector2 input)
    {
        _moveInput = input;
    }

    private void HandleMovement()
    {
        if (_characterController.isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -2f;
        }

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();
        
        Vector3 direction = forward * _moveInput.y + right * _moveInput.x;
        direction *= _movementConfig.targetMoveSpeed;

        _verticalVelocity += _gravity * _movementConfig.gravityMultiplier * Time.deltaTime;
        direction.y = _verticalVelocity;
        
        _characterController.Move(direction * Time.deltaTime);
    }

    private void CancelJump()
    {
        if (_verticalVelocity > 0)
        {
            _verticalVelocity *= 0.5f;
        }
    }

    private void Jump()
    {
        if (_characterController.isGrounded)
        {
            _verticalVelocity = _movementConfig.baseJumpForce;
        }
    }
    
    
    
}
