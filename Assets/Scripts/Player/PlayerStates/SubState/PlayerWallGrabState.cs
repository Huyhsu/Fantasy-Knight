using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    public PlayerWallGrabState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 WallClimb
        // 2 WallSlide
    }

    #region w/ Grab

    private Vector2 _holdPosition;

    private void HoldPosition()
    {
        Player.transform.position = _holdPosition;
        Core.Movement.SetVelocityX(0f);
        Core.Movement.SetVelocityY(0f);
    }

    #endregion
    
    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();

        _holdPosition = Player.transform.position;
        HoldPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsExitingState) return;

        HoldPosition();
        
        if (YInput > 0)
        {
            // WallClimb
            StateMachine.ChangeState(Player.WallClimbState);
        }
        else if (YInput < 0 || !GrabInput)
        {
            //WallSlide
            StateMachine.ChangeState(Player.WallSlideState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        // HoldPosition();
    }

    #endregion
}
