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

    #region w/ Variables

    // Input
    protected int XInput;
    protected int YInput;
    protected bool JumpInput;
    protected bool GrabInput;
    // Check
    protected bool IsGrounded;
    protected bool IsTouchingWall;
    protected bool IsTouchingLedge;

    #endregion
    
    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
        IsGrounded = Core.CollisionSenses.Ground;
        IsTouchingWall = Core.CollisionSenses.WallFront;
        IsTouchingLedge = Core.CollisionSenses.LedgeHorizontal;

        if (IsTouchingWall && !IsTouchingLedge)// 設定 LedgeClimbState 的觀察位置
        {
            Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        XInput = Player.InputHandler.NormalizedXInput;
        YInput = Player.InputHandler.NormalizedYInput;
        JumpInput = Player.InputHandler.JumpInput;
        GrabInput = Player.InputHandler.GrabInput;
        
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
        else if (IsTouchingWall && !IsTouchingLedge)
        {
            // LedgeClimb
            StateMachine.ChangeState(Player.LedgeClimbState);
        }
        
    }

    #endregion
}
