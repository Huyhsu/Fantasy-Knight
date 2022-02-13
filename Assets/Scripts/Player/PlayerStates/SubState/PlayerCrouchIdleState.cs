using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 CrouchMoveState
        // 2 IdleState
    }

    #region w/ State Workflow

    public override void Enter()
    {
        base.Enter();
        IsCrouchState = true;
        Core.Movement.SetVelocityZero();
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
        if(IsExitingState) return;
        
        if (XInput != 0)
        {
            // CrouchMove
            StateMachine.ChangeState(Player.CrouchMoveState);
        }
        else if (YInput != -1 && !IsTouchingCeiling)
        {
            // Idle
            StateMachine.ChangeState(Player.IdleState);
        }
    }

    #endregion
}
