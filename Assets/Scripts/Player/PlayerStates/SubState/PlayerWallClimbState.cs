using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 WallGrabState
    }

    #region w/ State Workflow

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsExitingState) return;

        Core.Movement.SetVelocityY(PlayerData.wallClimbVelocity);
        
        if (YInput != 1)
        {
            // WallGrab
            StateMachine.ChangeState(Player.WallGrabState);
        }
    }
    
    #endregion
    

}
