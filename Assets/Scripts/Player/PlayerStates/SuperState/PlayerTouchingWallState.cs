using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected PlayerTouchingWallState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 WallJumpState
        // 2 IdleState
        // 3 InAirState
        // 4 LedgeClimbState
    }

    private int _xInput;
    private bool _grabInput;
    
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isTouchingLedge;
    
    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();

        _isGrounded = Core.CollisionSenses.Ground;
        _isTouchingWall = Core.CollisionSenses.WallFront;
        _isTouchingLedge = Core.CollisionSenses.LedgeHorizontal;
        
        if (!_isTouchingLedge && _isTouchingWall)
        {
            // Check Position
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = Player.InputHandler.NormalizedXInput;
        _grabInput = Player.InputHandler.GrabInput;
        
        if (_isGrounded && !_grabInput)
        {
            // Idle
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (!_isTouchingWall && !_isGrounded || (_xInput != Core.Movement.FacingDirection && !_grabInput))
        {
            // InAir
            StateMachine.ChangeState(Player.InAirState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    #endregion
}
