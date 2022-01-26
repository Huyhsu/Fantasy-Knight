using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        // 1 IdleState
    }

    private int _xInput;
    
    #region w/ State Workflow

    public override void DoCheck()
    {
        base.DoCheck();
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
        if (IsExitingState) return;

        _xInput = Player.InputHandler.NormalizedXInput;
        
        Core.Movement.CheckIfShouldFlip(_xInput);
        Core.Movement.SetVelocityX(_xInput * PlayerData.movementVelocity);

        if (_xInput == 0)
        {
            // Idle
            StateMachine.ChangeState(Player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        // Core.Movement.SetVelocityX(XInput * PlayerData.movementVelocity);
    }

    #endregion
}
