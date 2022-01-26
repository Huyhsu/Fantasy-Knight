using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        AmountOfJumpsLeft = player.PlayerData.amountOfJumps;
    }

    #region w/ Jump

    public int AmountOfJumpsLeft;
    public bool CanJump => AmountOfJumpsLeft > 0;
    
    public void ResetAmountOfJumpsLeft() => AmountOfJumpsLeft = Player.PlayerData.amountOfJumps;
    public void DecreaseAmountOfJumpsLeft() => AmountOfJumpsLeft--;

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
        
        Core.Movement.SetVelocityY(PlayerData.jumpVelocity);
        IsAbilityDone = true;
    }

    #endregion
}
