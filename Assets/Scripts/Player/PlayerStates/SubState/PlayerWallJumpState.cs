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
    public void DetermineWallJumpDirection(bool isTouchingWall)// 若面對牆壁 WallJump 的方向須設定相反
    {
        _wallJumpDirection = isTouchingWall ? -Core.Movement.FacingDirection : Core.Movement.FacingDirection;
    }

    #endregion
    
    #region w/ State Workflow
    
    public override void Enter()
    {
        base.Enter();
        Core.Movement.SetVelocity(PlayerData.wallJumpVelocity, PlayerData.wallJumpAngle, _wallJumpDirection);
        Player.InputHandler.UseJumpInput();// 設定 JumpInput 為 false
        Player.JumpState.ResetAmountOfJumpsLeft();
        Player.JumpState.DecreaseAmountOfJumpsLeft();
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Core.Movement.CheckIfShouldFlip(_wallJumpDirection);
        Player.Animator.SetFloat("yVelocity", Core.Movement.CurrentVelocity.y);// Set up Jump/Fall Animation

        if (Time.time >= StartTime + PlayerData.wallJumpTime)// 若時間一結束 則可切換至其他狀態
        {
            IsAbilityDone = true;
        }
        
        if (Time.time > StartTime + PlayerData.wallJumpTime / 2 && IsTouchingWall)// 若時間已過半 且 已碰到另一牆 即可切換至其他狀態
        {
            IsAbilityDone = true;
        }
    }

    #endregion
}
