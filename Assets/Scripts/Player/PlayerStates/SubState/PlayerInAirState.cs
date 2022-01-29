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

    #region w/ Jump
    
    private bool _isJumping;
    private bool _isJumpCoyoteTime;
    
    // 由 JumpState 設定是否在跳躍
    public void SetIsJumping() => _isJumping = true;
    public void StartJumpCoyoteTime() => _isJumpCoyoteTime = true;
    // 若正在跳躍 根據 JumpInput 是否停止來實現不同跳躍高度
    private void CheckJumpMultiplier()
    {
        if (!_isJumping) return;
        
        if (JumpInputStop)
        {
            Core.Movement.SetVelocityY(Core.Movement.CurrentVelocity.y * PlayerData.variableJumpHeightMultiplier);
            _isJumping = false;
        }
        else if (Core.Movement.CurrentVelocity.y < 0.01f)
        {
            _isJumping = false;
        }
    }
    // 確認郊狼時間 若已經超過則減少跳躍次數
    private void CheckJumpCoyoteTime()
    {
        if (_isJumpCoyoteTime && Time.time >= StartTime + PlayerData.coyoteTime)
        {
            _isJumpCoyoteTime = false;
            Player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    #endregion

    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
        
        // 設定 LedgeClimbState 的觀察位置
        if (IsTouchingWall && !IsTouchingLedge)
        {
            Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        CheckJumpCoyoteTime();

        if (IsGrounded && Core.Movement.CurrentVelocity.y < 0.01f)
        {
            // Land
            StateMachine.ChangeState(Player.LandState);
        }
        else if (JumpInput && Player.JumpState.CanJump)
        {
            // Jump
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (IsTouchingWall && !IsTouchingLedge && !IsGrounded && YInput != -1)
        {
            // LedgeClimb
            StateMachine.ChangeState(Player.LedgeClimbState);
        }
        else if (IsTouchingWall && GrabInput && IsTouchingLedge)
        {
            // WallGrab
            StateMachine.ChangeState(Player.WallGrabState);
        }
        else if (IsTouchingWall && XInput == Core.Movement.FacingDirection && Core.Movement.CurrentVelocity.y < 0.01f)
        {
            // WallSlide
            StateMachine.ChangeState(Player.WallSlideState);
        }
        else
        {
            Core.Movement.CheckIfShouldFlip(XInput);

            // Set up Jump/Fall Animation
            Player.Animator.SetFloat("yVelocity", Core.Movement.CurrentVelocity.y);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        CheckJumpMultiplier();
        Core.Movement.SetVelocityX(XInput * PlayerData.movementVelocity);
    }

    #endregion
}
