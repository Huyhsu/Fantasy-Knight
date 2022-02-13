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
        IsCrouchState = true;
        Player.SetBoxColliderHeight(PlayerData.crouchColliderHeight);// 設定蹲下高度
    }

    public override void Exit()
    {
        base.Exit();
        IsCrouchState = false;
        Player.SetBoxColliderHeight(PlayerData.standColliderHeight);// 設定站起高度
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsExitingState) return;

        Core.Movement.CheckIfShouldFlip(XInput);
        Core.Movement.SetVelocityX(XInput * PlayerData.crouchMovementVelocity);
        
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

    #endregion
}
