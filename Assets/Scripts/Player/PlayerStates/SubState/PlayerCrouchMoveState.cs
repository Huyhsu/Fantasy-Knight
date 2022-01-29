using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 CrouchIdleState
        // 2 MoveState
    }

    #region w/ State Workflow

    public override void Enter()
    {
        base.Enter();
        
        // 設定蹲下高度
        Player.SetBoxColliderHeight(PlayerData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        
        // 設定站起高度
        Player.SetBoxColliderHeight(PlayerData.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsExitingState) return;

        Core.Movement.CheckIfShouldFlip(XInput);

        if (XInput == 0)
        {
            // CrouchIdle
            StateMachine.ChangeState(Player.CrouchIdleState);
        }
        else if (YInput != -1 && !IsTouchingCeiling)
        {
            // Move
            StateMachine.ChangeState(Player.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        Core.Movement.SetVelocityX(XInput * PlayerData.crouchMovementVelocity);
    }    

    #endregion
}
