using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    public PlayerWallJumpState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // Transition to InAirState
    }

    #region w/ Wall Jump

    private int _wallJumpDirection;
    // 若面對牆壁 WallJump 的方向須設定相反
    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        _wallJumpDirection = isTouchingWall ? -Core.Movement.FacingDirection : Core.Movement.FacingDirection;
    }

    #endregion
    
    #region w/ State Workflow
    
    public override void Enter()
    {
        base.Enter();
        // 設定 JumpInput 為 false
        Player.InputHandler.UseJumpInput();
        Player.JumpState.ResetAmountOfJumpsLeft();
        Player.JumpState.DecreaseAmountOfJumpsLeft();
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Core.Movement.CheckIfShouldFlip(_wallJumpDirection);
        
        Player.Animator.SetFloat("yVelocity", Core.Movement.CurrentVelocity.y);

        // 若時間一結束 則可切換至其他狀態
        if (Time.time >= StartTime + PlayerData.wallJumpTime)
        {
            IsAbilityDone = true;
        }

        // 若時間已過半 且 已碰到另一牆 即可切換至其他狀態
        if (Time.time > StartTime + PlayerData.wallJumpTime / 2 && IsTouchingWall)
        {
            IsAbilityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!ShouldDoInEnter) return;
        
        // 在 Enter 時設定跳躍速度
        Core.Movement.SetVelocity(PlayerData.wallJumpVelocity, PlayerData.wallJumpAngle, _wallJumpDirection);
        
        ShouldDoInEnter = false;

    }

    #endregion
}
