using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected PlayerGroundedState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 JumpState (AbilityState)
        // 2 InAirState (State)
    }

    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        
        Player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (JumpInput && Player.JumpState.CanJump)
        {
            // Jump
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (!IsGrounded)
        {
            // InAir
            Player.InAirState.StartJumpCoyoteTime();
            StateMachine.ChangeState(Player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }    

    #endregion

}
