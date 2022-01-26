using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 IdleState (GroundedState)
        // 2 JumpState (AbilityState)
        // 3 WallGrabState (TouchingWallState)
        // 4 WallSlideState (TouchingWallState)
    }

    private int _xInput;
    private bool _jumpInput;
    private bool _jumpInputStop;
    private bool _grabInput;
    
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isTouchingLedge;
    
    #region w/ Jump
    
    private bool _isJumping;
    private bool _isJumpCoyoteTime;
    
    public void SetIsJumping() => _isJumping = true;
    public void StartJumpCoyoteTime() => _isJumpCoyoteTime = true;
    
    private void CheckJumpMultiplier()
    {
        if (!_isJumping) return;
        
        if (_jumpInputStop)
        {
            Core.Movement.SetVelocityY(Core.Movement.CurrentVelocity.y * PlayerData.variableJumpHeightMultiplier);
            _isJumping = false;
        }
        else if (Core.Movement.CurrentVelocity.y < Mathf.Epsilon)
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

        _isGrounded = Core.CollisionSenses.Ground;
        _isTouchingWall = Core.CollisionSenses.WallFront;
        _isTouchingLedge = Core.CollisionSenses.LedgeHorizontal;
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

        _xInput = Player.InputHandler.NormalizedXInput;
        _jumpInput = Player.InputHandler.JumpInput;
        _jumpInputStop = Player.InputHandler.JumpInputStop;
        _grabInput = Player.InputHandler.GrabInput;

        CheckCoyoteTime();
        CheckJumpMultiplier();
        Core.Movement.SetVelocityX(_xInput * PlayerData.movementVelocity);
        
        Core.Movement.CheckIfShouldFlip(_xInput);
        
        if (_isGrounded && Core.Movement.CurrentVelocity.y < Mathf.Epsilon)
        {
            // Idle
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (_jumpInput && Player.JumpState.CanJump)
        {
            // Jump
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            // WallGrab
            StateMachine.ChangeState(Player.WallGrabState);
        }
        else if (_isTouchingWall && _xInput == Core.Movement.FacingDirection && Core.Movement.CurrentVelocity.y < 0)
        {
            // WallSlide
            StateMachine.ChangeState(Player.WallSlideState);
        }
        else
        {
            // Set up Jump/Fall Animation
            Player.Animator.SetFloat("yVelocity", Core.Movement.CurrentVelocity.y);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        // CheckJumpMultiplier();
        // Core.Movement.SetVelocityX(XInput * PlayerData.movementVelocity);
    }

    #endregion
}
