using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // Transition to InAirState
        
        _amountOfJumpsLeft = player.PlayerData.amountOfJumps;
    }

    #region w/ Jump

    private int _amountOfJumpsLeft;
    public bool CanJump => _amountOfJumpsLeft > 0;
    public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = Player.PlayerData.amountOfJumps;// 重設跳躍次數
    public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;// 減少跳躍次數

    #endregion
    
    #region w/ State Workflow

    public override void Enter()
    {
        base.Enter();
        Core.Movement.SetVelocityY(PlayerData.jumpVelocity);
        IsAbilityDone = true;
        DecreaseAmountOfJumpsLeft();
        Player.InputHandler.UseJumpInput();// 設定 JumpInput 為 false
        Player.InAirState.SetIsJumping();// 在 InAirState 設定正在跳躍 _isJumping 用來確認不同跳躍高度
    }

    #endregion
}
