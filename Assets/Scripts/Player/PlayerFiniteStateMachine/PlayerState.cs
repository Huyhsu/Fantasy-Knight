using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    #region w/ Input Variables

    protected int XInput;
    protected int YInput;
    protected bool JumpInput;
    protected bool JumpInputStop;
    protected bool GrabInput;

    #endregion

    #region w/ Collision Senses Variables

    protected bool IsGrounded;
    protected bool IsTouchingWall;
    protected bool IsTouchingWallBack;
    protected bool IsTouchingLedge;

    #endregion
    
    #region w/ Components
    
    protected Player Player { get; private set; }
    
    protected Core Core { get; private set; }
    protected PlayerData PlayerData { get; private set; }
    protected PlayerStateMachine StateMachine { get; private set; }

    // State Action Bools
    protected bool IsAnimationFinished;
    protected bool IsExitingState;

    protected float StartTime;

    private readonly string _animationBoolName;

    #endregion

    #region w/ Constructor

    protected PlayerState(Player player, string animationBoolName)
    {
        Player = player;
        _animationBoolName = animationBoolName;
        
        Core = player.Core;
        PlayerData = player.PlayerData;
        StateMachine = player.StateMachine;
    }    

    #endregion
    
    #region w/ State Workflow

    public virtual void DoCheck()
    {
        // Check Collision Senses Variables
        IsGrounded = Core.CollisionSenses.Ground;
        IsTouchingWall = Core.CollisionSenses.WallFront;
        IsTouchingWallBack = Core.CollisionSenses.WallBack;
        IsTouchingLedge = Core.CollisionSenses.LedgeHorizontal;
    }

    public virtual void Enter()
    {
        DoCheck();
        Player.Animator.SetBool(_animationBoolName,true);
        StartTime = Time.time;
        IsAnimationFinished = false;
        IsExitingState = false;
    }

    public virtual void Exit()
    {
        Player.Animator.SetBool(_animationBoolName, false);
        IsExitingState = true;
    }

    public virtual void LogicUpdate()
    {
        // Check Input Variables
        XInput = Player.InputHandler.NormalizedXInput;
        YInput = Player.InputHandler.NormalizedYInput;
        JumpInput = Player.InputHandler.JumpInput;
        JumpInputStop = Player.InputHandler.JumpInputStop;
        GrabInput = Player.InputHandler.GrabInput;
    }

    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }    

    #endregion

    #region w/ Animation Trigger Functions

    public virtual void AnimationTrigger() { }
    public virtual void AnimationFinishTrigger() => IsAnimationFinished = true;

    #endregion
}
