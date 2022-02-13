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

    #region w/ Variables

    // Input
    protected int XInput;
    protected int YInput;
    protected bool JumpInput;
    protected bool GrabInput;
    protected bool AttackInput;
    // Check
    protected bool IsGrounded;
    protected bool IsTouchingWall;
    protected bool IsTouchingLedge;
    protected bool IsTouchingCeiling;
    // State Check
    protected bool IsCrouchState;
    
    #endregion
    
    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
        IsGrounded = Core.CollisionSenses.Ground;
        IsTouchingWall = Core.CollisionSenses.WallFront;
        IsTouchingLedge = Core.CollisionSenses.LedgeHorizontal;
        IsTouchingCeiling = Core.CollisionSenses.Ceiling;
    }

    public override void Enter()
    {
        base.Enter();
        IsCrouchState = false;
        Player.JumpState.ResetAmountOfJumpsLeft();// 重設跳躍次數
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        XInput = Player.InputHandler.NormalizedXInput;
        YInput = Player.InputHandler.NormalizedYInput;
        JumpInput = Player.InputHandler.JumpInput;
        GrabInput = Player.InputHandler.GrabInput;
        AttackInput = Player.InputHandler.AttackInput;

        if (AttackInput && IsCrouchState)
        {
            // SwordAttack - Crouch
            Player.SwordAttackState.SetIsCrouching();
            StateMachine.ChangeState(Player.SwordAttackState);
        }
        else if (AttackInput)
        {
            // SwordAttack
            Player.SwordAttackState.SetIsNotCrouching();
            StateMachine.ChangeState(Player.SwordAttackState);
        }
        else if (JumpInput && Player.JumpState.CanJump && !IsTouchingCeiling)
        {
            // Jump
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (!IsGrounded)
        {
            // InAir
            Player.InAirState.StartJumpCoyoteTime();// 在 InAirState 設定郊狼時間為 true
            StateMachine.ChangeState(Player.InAirState);
        }
        else if (IsTouchingWall && GrabInput && IsTouchingLedge)
        {
            // WallGrab
            StateMachine.ChangeState(Player.WallGrabState);
        }
    }

    #endregion

}
