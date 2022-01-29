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

    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        // 重設跳躍次數
        Player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (JumpInput && Player.JumpState.CanJump && !IsTouchingCeiling)
        {
            // Jump
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (!IsGrounded)
        {
            // InAir
            // 在 InAirState 設定郊狼時間為 true
            Player.InAirState.StartJumpCoyoteTime();
            StateMachine.ChangeState(Player.InAirState);
        }
        else if (IsTouchingWall && GrabInput && IsTouchingLedge)
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
