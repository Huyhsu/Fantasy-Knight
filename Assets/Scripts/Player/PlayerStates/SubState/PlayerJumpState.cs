using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        _amountOfJumpsLeft = player.PlayerData.amountOfJumps;
    }

    #region w/ Jump

    private int _amountOfJumpsLeft;
    public bool CanJump => _amountOfJumpsLeft > 0;
    
    public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = Player.PlayerData.amountOfJumps;
    public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;

    #endregion
    
    #region w/ State Workflow

    public override void Enter()
    {
        base.Enter();
        Player.InputHandler.UseJumpInput();
        DecreaseAmountOfJumpsLeft();
        Player.InAirState.SetIsJumping();
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!ShouldDoInEnter) return;
        
        Core.Movement.SetVelocityY(PlayerData.jumpVelocity);
        IsAbilityDone = true;
        
        ShouldDoInEnter = false;
    }

    #endregion
}
