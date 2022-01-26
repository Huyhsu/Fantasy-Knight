using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    // Delay Check Time
    [SerializeField] private float inputHoldTime = 0.2f;

    #region w/ Components

    private PlayerInput _playerInput;

    #endregion
    
    #region w/ Input Start Time

    private float _jumpInputStartTime;

    #endregion

    #region w/ Input Variables

    private Vector2 _rawMovementInput;
    public int NormalizedXInput { get; private set; }
    public int NormalizedYInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }    

    #endregion

    #region w/ Unity Callback Functions

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
    }    

    #endregion

    #region w/ Movement

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        _rawMovementInput = context.ReadValue<Vector2>();
        NormalizedXInput = Mathf.RoundToInt(_rawMovementInput.x);
        NormalizedYInput = Mathf.RoundToInt(_rawMovementInput.y);
    }

    #endregion

    #region w/ Jump

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            _jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    #endregion
}
