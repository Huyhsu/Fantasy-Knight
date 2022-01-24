using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool IsAbilityDone;
    protected bool IsGrounded;

    protected bool JumpInput;

    protected PlayerAbilityState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
    }

    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
        IsGrounded = Core.CollisionSenses.Ground;
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
        if (!IsAbilityDone) return;

        if (IsGrounded && Core.Movement.CurrentVelocity.y < 0.01f)
        {
            // StateMachine.ChangeState();
        }
        else
        {
            
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }    

    #endregion

}
