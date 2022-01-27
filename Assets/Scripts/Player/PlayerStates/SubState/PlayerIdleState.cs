using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 MoveState
    }

    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsExitingState) return;

        if (XInput != 0)
        {
            // Move
            StateMachine.ChangeState(Player.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        Core.Movement.SetVelocityX(0f);
    }

    #endregion
}
