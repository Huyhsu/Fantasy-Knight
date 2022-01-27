using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected PlayerTouchingWallState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 WallJumpState
        // 2 IdleState (GroundedState)
        // 3 InAirState (State)
        // 4 LedgeClimbState (State)
    }

    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();

        if (!IsTouchingLedge && IsTouchingWall)
        {
            // Check Ledge Climb Position
        }
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

        if (JumpInput)
        {
            // WallJump
            Player.WallJumpState.DetermineWallJumpDirection(IsTouchingWall);
            StateMachine.ChangeState(Player.WallJumpState);
        }
        else if (IsGrounded && !GrabInput)
        {
            // Idle
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (!IsTouchingWall && !IsGrounded || (XInput != Core.Movement.FacingDirection && !GrabInput))
        {
            // InAir
            StateMachine.ChangeState(Player.InAirState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    #endregion
}
