using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected PlayerGroundedState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 JumpState (AbilityState)
        // 2 InAirState (State)
        // 3 WallGrab (TouchingWallState)
    }

    private bool _jumpInput;
    private bool _grabInput;
    
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isTouchingLedge;
    
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
        
        Player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _jumpInput = Player.InputHandler.JumpInput;
        _grabInput = Player.InputHandler.GrabInput;
        
        if (_jumpInput && Player.JumpState.CanJump)
        {
            // Jump
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (!_isGrounded)
        {
            // InAir
            Player.InAirState.StartJumpCoyoteTime();
            StateMachine.ChangeState(Player.InAirState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            // WallGrab
            StateMachine.ChangeState(Player.WallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }    

    #endregion

}
