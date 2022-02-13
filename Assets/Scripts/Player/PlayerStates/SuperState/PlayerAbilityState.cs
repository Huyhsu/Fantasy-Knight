using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected PlayerAbilityState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 CrouchIdleState (GroundedState)
        // 2 IdleState (GroundedState)
        // 3 InAirState (State)
    }

    #region w/ Variables
    
    // Input
    protected int XInput;
    // Check
    protected bool IsGrounded;
    protected bool IsTouchingWall;
    // Ability Check
    protected bool IsAbilityDone;

    #endregion

    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
        IsGrounded = Core.CollisionSenses.Ground;
        IsTouchingWall = Core.CollisionSenses.WallFront;
    }

    public override void Enter()
    {
        base.Enter();
        IsAbilityDone = false;
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!IsAbilityDone) return;
        XInput = Player.InputHandler.NormalizedXInput;

        if (IsGrounded && Core.Movement.CurrentVelocity.y < 0.01f && Player.SwordAttackState.IsCrouching)
        {
            // CrouchIdle
            StateMachine.ChangeState(Player.CrouchIdleState);
        }
        else 
        if (IsGrounded && Core.Movement.CurrentVelocity.y < 0.01f)
        {
            // Idle
            // Player.SwordCrouchAttackState.SetIsNotCrouching();
            StateMachine.ChangeState(Player.IdleState);
        }
        else
        {
            // InAir
            StateMachine.ChangeState(Player.InAirState);
        }
    }

    #endregion

}
