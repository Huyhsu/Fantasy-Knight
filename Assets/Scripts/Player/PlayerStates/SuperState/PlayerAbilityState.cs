using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected PlayerAbilityState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 IdleState (GroundedState)
        // 2 InAirState (State)
    }

    #region w/ Variables
    
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
        
        if (IsGrounded && Core.Movement.CurrentVelocity.y < 0.01f)
        {
            // Idle
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
