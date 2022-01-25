using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
    }

    #region w/ Input Variables

    private int _xInput;
    private bool _jumpInput;
    private bool _jumpInputStop;

    #endregion

    #region w/ Check Variables

    private bool _isGrounded;
    private bool _isJumping;
    private bool _isCoyoteTime;

    #endregion
    
    #region w/ Jump

    public void StartCoyoteTime() => _isCoyoteTime = true;

    public void SetIsJumping() => _isJumping = true;

    private void CheckJumpMultiplier()
    {
        if (!_isJumping) return;
        
        if (_jumpInputStop)
        {
            Core.Movement.SetVelocityY(Core.Movement.CurrentVelocity.y * PlayerData.variableJumpHeightMultiplier);
            _isJumping = false;
        }
        else if (Core.Movement.CurrentVelocity.y <= 0f)
        {
            _isJumping = false;
        }
    }
    
    private void CheckCoyoteTime()
    {
        if (_isCoyoteTime && Time.time >= StartTime + PlayerData.coyoteTime)
        {
            _isCoyoteTime = false;
            Player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    #endregion

    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
        _isGrounded = Core.CollisionSenses.Ground;
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
        
        _xInput = Player.InputHandler.NormalizedXInput;
        _jumpInput = Player.InputHandler.JumpInput;
        _jumpInputStop = Player.InputHandler.JumpInputStop;
        
        CheckJumpMultiplier();

        if (_isGrounded && Core.Movement.CurrentVelocity.y < 0.01f)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (_jumpInput && Player.JumpState.CanJump())
        {
            StateMachine.ChangeState(Player.JumpState);
        }
        else
        {
            Core.Movement.CheckIfShouldFlip(_xInput);
            Core.Movement.SetVelocityX(_xInput * PlayerData.movementVelocity);
            
            Player.Animator.SetFloat("yVelocity", Core.Movement.CurrentVelocity.y);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    #endregion
}
