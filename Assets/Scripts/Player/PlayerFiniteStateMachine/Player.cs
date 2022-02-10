using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region w/ Player Data

    [SerializeField] private PlayerData playerData;

    public PlayerData PlayerData
    {
        get => playerData;
        private set => playerData = value;
    }

    #endregion
    
    #region w/ State Variables

    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }

    #endregion

    #region w/ Components

    public Core Core { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    
    public Animator Animator { get; private set; }
    public BoxCollider2D MovementBoxCollider2D { get; private set; }

    #endregion

    #region w/ Set Collider Function

    private Vector2 _workspace;
    public void SetBoxColliderHeight(float height)// 設定蹲下或站起的高度
    {
        Vector2 center = MovementBoxCollider2D.offset;
        _workspace.Set(MovementBoxCollider2D.size.x, height);
        center.y += (height - MovementBoxCollider2D.size.y) / 2;

        MovementBoxCollider2D.size = _workspace;
        MovementBoxCollider2D.offset = center;
    }

    #endregion
    
    #region w/ Unity Callback Functions

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, "idle");
        MoveState = new PlayerMoveState(this, "move");
        JumpState = new PlayerJumpState(this, "inAir");
        InAirState = new PlayerInAirState(this, "inAir");
        WallSlideState = new PlayerWallSlideState(this, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, "inAir");
        LandState = new PlayerLandState(this, "move");
        LedgeClimbState = new PlayerLedgeClimbState(this, "ledgeClimbState");
        CrouchIdleState = new PlayerCrouchIdleState(this, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, "crouchMove");
    }

    private void Start()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        Animator = GetComponent<Animator>();
        MovementBoxCollider2D = GetComponent<BoxCollider2D>();
        
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion
    
    #region w/ Animation Trigger Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    #endregion
}
