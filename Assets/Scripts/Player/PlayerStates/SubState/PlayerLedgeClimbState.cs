using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    public PlayerLedgeClimbState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 IdleState (GroundedState)
        // 2 InAirState (State)
    }

    #region w/ Variables

    // Input
    private int _xInput;
    private int _yInput;

    #endregion
    
    #region w/ LedgeClimb

    private Vector2 _detectedPosition;
    private Vector2 _cornerPosition;
    private Vector2 _startPosition;
    private Vector2 _stopPosition;
    private Vector2 _workspace;

    private bool _isHanging;
    private bool _isClimbing;
    private bool _isTouchingCeiling;

    public void SetDetectedPosition(Vector2 position) => _detectedPosition = position;// 設定觀察位置 (從其他 State 中設定)
    
    private Vector2 DetermineCornerPosition()// 計算轉角位置
    {
        RaycastHit2D xHit = Physics2D.Raycast(Core.CollisionSenses.WallCheck.position, Vector2.right * Core.Movement.FacingDirection,
            Core.CollisionSenses.WallCheckDistance, Core.CollisionSenses.WhatIsGround);
        float xDistance = xHit.distance;
        _workspace.Set((xDistance + 0.015f) * Core.Movement.FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(Core.CollisionSenses.LedgeHorizontalCheck.position + (Vector3) (_workspace), Vector2.down,
            Core.CollisionSenses.LedgeHorizontalCheck.position.y - Core.CollisionSenses.WallCheck.position.y + 0.015f, Core.CollisionSenses.WhatIsGround);
        float yDistance = yHit.distance;
        
        _workspace.Set(Core.CollisionSenses.WallCheck.position.x + (xDistance * Core.Movement.FacingDirection), Core.CollisionSenses.LedgeHorizontalCheck.position.y - yDistance);
        return _workspace;
    }
    
    private void CheckForSpace()// 確認上方空間 (Idle or Crouch)
    {
        _isTouchingCeiling = Physics2D.Raycast(
            _cornerPosition + (Vector2.up * 0.015f) + (Vector2.right * 0.015f * Core.Movement.FacingDirection), Vector2.up,
            PlayerData.standColliderHeight, Core.CollisionSenses.WhatIsGround);
        
        Player.Animator.SetBool("isTouchingCeiling", _isTouchingCeiling);
    }
    
    #endregion
    
    #region w/ State Workflow
    
    public override void Enter()
    {
        base.Enter();
        
        Core.Movement.SetVelocityZero();
        Player.transform.position = _detectedPosition;// 觀察位置
        
        _cornerPosition = DetermineCornerPosition();// 轉角位置
        _startPosition.Set(_cornerPosition.x - (Core.Movement.FacingDirection * PlayerData.startPositionOffset.x),
            _cornerPosition.y - PlayerData.startPositionOffset.y);// 起始位置 (Hanging)
        _stopPosition.Set(_cornerPosition.x + (Core.Movement.FacingDirection * PlayerData.stopPositionOffset.x),
            _cornerPosition.y + PlayerData.stopPositionOffset.y);// 終點位置

        Player.transform.position = _startPosition;
    }

    public override void Exit()
    {
        base.Exit();

        _isHanging = false;
        
        if (IsAnimationFinished)// ChangeState 之後將位置設定為終點位置
        {
            Player.transform.position = _stopPosition;
            _isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _xInput = Player.InputHandler.NormalizedXInput;
        _yInput = Player.InputHandler.NormalizedYInput;

        if (IsAnimationFinished)// 若播完動畫 (Hold -> Climb)
        {
            if (_isTouchingCeiling)// 確認上方有無牆壁
            {
                // CrouchIdle
                StateMachine.ChangeState(Player.CrouchIdleState);
            }
            else
            {
                // Idle
                StateMachine.ChangeState(Player.IdleState);
            }
        }
        else// 沒爬上去
        {
            Core.Movement.SetVelocityZero();// 固定位置
            Player.transform.position = _startPosition;
            if (_xInput == Core.Movement.FacingDirection && _isHanging && !_isClimbing)// 往前方移動則由 Hanging 轉為 Climbing
            {
                CheckForSpace();
                _isClimbing = true;
                Player.Animator.SetBool("ledgeClimb", true);
            }
            else if (_yInput == -1 && _isHanging && !_isClimbing)// 由 Hanging 往下落
            {
                // InAir
                StateMachine.ChangeState(Player.InAirState);
            }
        }
    }
    
    #endregion

    #region w/ Animation Trigger Functions

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        _isHanging = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        
        Player.Animator.SetBool("ledgeClimb", false);
    }

    #endregion
}
