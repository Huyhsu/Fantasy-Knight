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

    protected bool IsAbilityDone;

    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        
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
            if (IsGrounded && CurrentVelocity.y < Mathf.Epsilon)
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
