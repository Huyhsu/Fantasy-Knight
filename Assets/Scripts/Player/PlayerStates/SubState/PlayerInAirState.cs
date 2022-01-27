using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 LandState (GroundedState)
        // 2 JumpState (AbilityState)
        // 3 WallGrabState (TouchingWallState)
        // 4 WallSlideState (TouchingWallState)
    }

    #region w/ Jump
    
    private bool _isJumping;
    private bool _isJumpCoyoteTime;
    
    public void SetIsJumping() => _isJumping = true;
    public void StartJumpCoyoteTime() => _isJumpCoyoteTime = true;
    
    private void CheckJumpMultiplier()
    {
        if (!_isJumping) return;
        
        if (JumpInputStop)
        {
            Core.Movement.SetVelocityY(CurrentVelocity.y * PlayerData.variableJumpHeightMultiplier);
            _isJumping = false;
        }
        else if (CurrentVelocity.y < Mathf.Epsilon)
        {
            _isJumping = false;
        }
    }
    
    private void CheckCoyoteTime()
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
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        CheckCoyoteTime();
        // CheckJumpMultiplier();

        Core.Movement.CheckIfShouldFlip(XInput);

        if (IsGrounded && CurrentVelocity.y < Mathf.Epsilon)
        {
            // Land
            StateMachine.ChangeState(Player.LandState);
        }
        else if (JumpInput && Player.JumpState.CanJump)
        {
            // Jump
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (IsTouchingWall && GrabInput && IsTouchingLedge)
        {
            // WallGrab
            StateMachine.ChangeState(Player.WallGrabState);
        }
        else if (IsTouchingWall && XInput == FacingDirection && CurrentVelocity.y < 0)
        {
            // WallSlide
            StateMachine.ChangeState(Player.WallSlideState);
        }
        else
        {
            // Set up Jump/Fall Animation
            Player.Animator.SetFloat("yVelocity", CurrentVelocity.y);
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
