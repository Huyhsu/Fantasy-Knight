using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 IdleState
    }

    #region w/ State Workflow
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsExitingState) return;

        Core.Movement.CheckIfShouldFlip(XInput);

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

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        Core.Movement.SetVelocityX(XInput * PlayerData.movementVelocity);
    }

    #endregion
}
