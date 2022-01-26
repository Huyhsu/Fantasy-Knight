using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 WallGrabState
    }

    private int _yInput;
    private bool _grabInput;
    
    #region w/ State Workflow

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsExitingState) return;

        _yInput = Player.InputHandler.NormalizedYInput;
        _grabInput = Player.InputHandler.GrabInput;
        
        Core.Movement.SetVelocityY(-PlayerData.wallSlideVelocity);

        if (_grabInput && _yInput == 0)
        {
            // WallGrab
            StateMachine.ChangeState(Player.WallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        // Core.Movement.SetVelocityY(-PlayerData.wallSlideVelocity);
    }    

    #endregion
}
