using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 MoveState
        // 2 IdleState
    }

    #region w/ State Workflow

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsExitingState) return;

        if (XInput != 0)
        {
            // Move
            StateMachine.ChangeState(Player.MoveState);
        }
        else
        {
            // Idle
            StateMachine.ChangeState(Player.IdleState);
        }
    }    

    #endregion
}
