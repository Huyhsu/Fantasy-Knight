using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected PlayerTouchingWallState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 WallJumpState
        // 2 IdleState
        // 3 InAirState
        // 4 LedgeClimbState
    }

    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();

        if (!IsTouchingLedge && IsTouchingWall)
        {
            // Check Position
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

        if (IsGrounded && !GrabInput)
        {
            // Idle
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (!IsTouchingWall || (XInput != FacingDirection && !GrabInput))
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
