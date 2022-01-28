using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    public PlayerLedgeClimbState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 IdleState (GroundedState)
        // 2 InAirState (State)
        // 3 WallJumpState (AbilityState)
    }

    #region w/ LedgeClimb

    private Vector2 _detectedPosition;
    private Vector2 _cornerPosition;
    private Vector2 _startPosition;
    private Vector2 _stopPosition;
    private Vector2 _workspace;

    private bool _isHanging;
    private bool _isClimbing;
    private bool _isTouchingCeiling;
    
    // 設定觀察位置 (從其他 State 中設定)
    public void SetDetectedPosition(Vector2 position) => _detectedPosition = position;
    
    // 計算轉角位置
    private Vector2 DetermineCornerPosition()
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

    // 確認上方空間 (Idle or Crouch)
    private void CheckForSpace()
    {
        _isTouchingCeiling = Physics2D.Raycast(
            _cornerPosition + (Vector2.up * 0.015f) + (Vector2.right * 0.015f * Core.Movement.FacingDirection), Vector2.up,
            PlayerData.standColliderHeight, Core.CollisionSenses.WhatIsGround);
        Player.Animator.SetBool("isTouchingCeiling", _isTouchingCeiling);
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
        
        Core.Movement.SetVelocityZero();
        // 觀察位置
        Player.transform.position = _detectedPosition;
        // 轉角位置
        _cornerPosition = DetermineCornerPosition();
        // 起始位置 (Hanging)
        _startPosition.Set(_cornerPosition.x - (Core.Movement.FacingDirection * PlayerData.startPositionOffset.x),
            _cornerPosition.y - PlayerData.startPositionOffset.y);
        // 終點位置
        _stopPosition.Set(_cornerPosition.x + (Core.Movement.FacingDirection * PlayerData.stopPositionOffset.x),
            _cornerPosition.y + PlayerData.stopPositionOffset.y);

        Player.transform.position = _startPosition;
    }

    public override void Exit()
    {
        base.Exit();

        _isHanging = false;

        // ChangeState 之後將位置設定為終點位置
        if (_isClimbing && IsAnimationFinished)
        {
            Player.transform.position = _stopPosition;
            _isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        // 若播完動畫 (Hold -> Climb)
        if (IsAnimationFinished)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        // 沒爬上去
        else
        {
            // 固定位置
            Core.Movement.SetVelocityZero();
            Player.transform.position = _startPosition;

            // 往前方移動則由 Hanging 轉為 Climbing
            if (XInput == Core.Movement.FacingDirection && _isHanging && !_isClimbing)
            {
                CheckForSpace();
                _isClimbing = true;
                Player.Animator.SetBool("ledgeClimb", true);
            }
            // 由 Hanging 往下落
            else if (YInput == -1 && _isHanging && !_isClimbing)
            {
                // InAir
                StateMachine.ChangeState(Player.InAirState);
            }
            else if (JumpInput && !_isClimbing)
            {
                // WallJump
                Player.WallJumpState.DetermineWallJumpDirection(true);
                StateMachine.ChangeState(Player.WallJumpState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
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
