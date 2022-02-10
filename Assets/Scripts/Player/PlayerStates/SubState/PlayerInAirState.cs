using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 LandState (GroundedState)
        // 2 JumpState (AbilityState)
        // 3 LedgeClimbState (State)
        // 4 WallGrabState (TouchingWallState)
        // 5 WallSlideState (TouchingWallState)
    }

    #region w/ Variables

    // Input
    private int _xInput;
    private int _yInput;
    private bool _jumpInput;
    private bool _jumpInputStop;
    private bool _grabInput;
    // Check
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isTouchingLedge;

    #endregion
    
    #region w/ Jump
    
    private bool _isJumping;
    private bool _isJumpCoyoteTime;
    public void SetIsJumping() => _isJumping = true;// 由 JumpState 設定是否在跳躍
    public void StartJumpCoyoteTime() => _isJumpCoyoteTime = true;
    private void CheckJumpMultiplier()// 若正在跳躍 根據 JumpInput 是否停止來實現不同跳躍高度
    {
        if (!_isJumping) return;
        
        if (_jumpInputStop)
        {
            Core.Movement.SetVelocityY(Core.Movement.CurrentVelocity.y * PlayerData.variableJumpHeightMultiplier);
            _isJumping = false;
        }
        else if (Core.Movement.CurrentVelocity.y < 0.01f)
        {
            _isJumping = false;
        }
    }
    private void CheckJumpCoyoteTime()// 確認郊狼時間 若超過時間 則減少跳躍次數
    {
        if (!_isJumpCoyoteTime || !(Time.time >= StartTime + PlayerData.coyoteTime)) return;
        _isJumpCoyoteTime = false;
        Player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    #endregion

    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
        _isGrounded = Core.CollisionSenses.Ground;
        _isTouchingWall = Core.CollisionSenses.WallFront;
        _isTouchingLedge = Core.CollisionSenses.LedgeHorizontal;

        if (_isTouchingWall && !_isTouchingLedge)// 設定 LedgeClimbState 的觀察位置
        {
            Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _xInput = Player.InputHandler.NormalizedXInput;
        _yInput = Player.InputHandler.NormalizedYInput;
        _jumpInput = Player.InputHandler.JumpInput;
        _jumpInputStop = Player.InputHandler.JumpInputStop;
        _grabInput = Player.InputHandler.GrabInput;
        
        CheckJumpCoyoteTime();
        CheckJumpMultiplier();

        if (_isGrounded && Core.Movement.CurrentVelocity.y < 0.01f)
        {
            // Land
            StateMachine.ChangeState(Player.LandState);
        }
        else if (_jumpInput && Player.JumpState.CanJump)
        {
            // Jump
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (_isTouchingWall && !_isTouchingLedge && !_isGrounded && _yInput != -1)
        {
            // LedgeClimb
            StateMachine.ChangeState(Player.LedgeClimbState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            // WallGrab
            StateMachine.ChangeState(Player.WallGrabState);
        }
        else if (_isTouchingWall && _xInput == Core.Movement.FacingDirection && Core.Movement.CurrentVelocity.y < 0.01f)
        {
            // WallSlide
            StateMachine.ChangeState(Player.WallSlideState);
        }
        else
        {
            Core.Movement.SetVelocityX(_xInput * PlayerData.movementVelocity);
            Core.Movement.CheckIfShouldFlip(_xInput);

            Player.Animator.SetFloat("yVelocity", Core.Movement.CurrentVelocity.y);// Set up Jump/Fall Animation
        }
    }

    #endregion
}
