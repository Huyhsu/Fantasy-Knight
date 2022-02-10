using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 WallGrabState
    }

    #region w/ State Workflow

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsExitingState) return;
        
        Core.Movement.SetVelocityY(-PlayerData.wallSlideVelocity);
        
        if (GrabInput && YInput == 0)
        {
            // WallGrab
            StateMachine.ChangeState(Player.WallGrabState);
        }
    }

    #endregion
}
