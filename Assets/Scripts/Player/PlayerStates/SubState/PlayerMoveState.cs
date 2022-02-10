using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 IdleState
        // 2 CrouchMoveState
    }

    #region w/ State Workflow
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsExitingState) return;

        Core.Movement.CheckIfShouldFlip(XInput);
        Core.Movement.SetVelocityX(XInput * PlayerData.movementVelocity);
        
        if (XInput == 0)
        {
            // Idle
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (YInput == -1)
        {
            // CrouchMove
            StateMachine.ChangeState(Player.CrouchMoveState);
        }
    }

    #endregion
}
