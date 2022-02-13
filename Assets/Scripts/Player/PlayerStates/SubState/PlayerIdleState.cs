using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 MoveState
        // 2 CrouchIdleState
    }

    #region w/ State Workflow

    public override void Enter()
    {
        base.Enter();
        Core.Movement.SetVelocityX(0f);
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
        else if (YInput == -1)
        {
            //CrouchIdle
            StateMachine.ChangeState(Player.CrouchIdleState);
        }
    }

    #endregion
}
