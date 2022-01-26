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
    private bool _isGrounded;
    
    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
        
        _isGrounded = Core.CollisionSenses.Ground;
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
            if (_isGrounded && Core.Movement.CurrentVelocity.y < Mathf.Epsilon)
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
