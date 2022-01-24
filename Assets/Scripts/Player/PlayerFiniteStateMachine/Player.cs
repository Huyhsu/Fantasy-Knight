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

    #endregion

    #region w/ Components

    public Core Core { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Animator Animator { get; private set; }

    #endregion

    #region w/ Unity Callback Functions

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();
    }

    private void Start()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        Animator = GetComponent<Animator>();
        
        // StateMachine.Initialize();
    }

    private void Update()
    {
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
