using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    #region w/ Components
    
    protected Player Player { get; private set; }
    
    protected Core Core { get; private set; }
    protected PlayerData PlayerData { get; private set; }
    protected PlayerStateMachine StateMachine { get; private set; }

    // State Booleans
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

    public virtual void DoCheck() { }

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
    
    public virtual void LogicUpdate() { }
    
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
