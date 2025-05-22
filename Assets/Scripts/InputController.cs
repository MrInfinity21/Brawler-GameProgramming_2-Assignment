using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private InputSystem_Actions _gameControls;

    public event Action JumpEvent;
    public event Action JumpUpEventCanceled;
    public event Action<Vector2> MoveEvent;

    public event Action AttackEvent;
    public event Action AttackEventCanceled;

    private void Awake()
    {
        _gameControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _gameControls.Player.Enable();

        _gameControls.Player.Move.performed += OnMovePerformed;
        _gameControls.Player.Move.canceled += OnMoveCancelled;
        _gameControls.Player.Jump.performed += OnJumpPerformed;
        _gameControls.Player.Jump.canceled += OnJumpCanceled;
        _gameControls.Player.Attack.performed += OnAttackPerformed;
        _gameControls.Player.Attack.canceled += OnAttackPerformed;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        AttackEvent?.Invoke();
    }
    
    private void OnDisable()
    {
        _gameControls.Player.Enable();

        _gameControls.Player.Move.performed -= OnMovePerformed;
        _gameControls.Player.Move.canceled -= OnMoveCancelled;
        _gameControls.Player.Jump.performed -= OnJumpPerformed;
        _gameControls.Player.Jump.canceled -= OnJumpCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnMoveCancelled(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(Vector2.zero);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        JumpEvent?.Invoke();
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        JumpUpEventCanceled?.Invoke();
    }


}
