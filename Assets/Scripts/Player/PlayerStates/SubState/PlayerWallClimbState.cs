using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 WallGrabState
    }

    private int _yInput;
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsExitingState) return;

        _yInput = Player.InputHandler.NormalizedYInput;
        
        Core.Movement.SetVelocityY(PlayerData.wallClimbVelocity);

        if (_yInput != 1)
        {
            // WallGrab
            StateMachine.ChangeState(Player.WallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        // Core.Movement.SetVelocityY(PlayerData.wallClimbVelocity);
    }
}
