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

    protected bool IsAbilityDone;
    protected bool ShouldDoInEnter;    

    #endregion

    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        ShouldDoInEnter = true;
        IsAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsAbilityDone)
        {
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }    

    #endregion

}
