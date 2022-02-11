using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    public PlayerAttackState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
    }

    #region w/ Components

    private Weapon _weapon;

    #endregion

    #region w/ Variables

    private float _velocityToSet;

    private bool _shouldSetVelocity;
    private bool _shouldCheckFlip;

    #endregion

    #region w/ Weapon

    public void SetWeapon(Weapon weapon)// 設定 weapon
    {
        _weapon = weapon;
        _weapon.InitializeWeapon(this, Core);
    }

    public void SetPlayerVelocity(float velocity)// 設定 velocity
    {
        Core.Movement.SetVelocityX(velocity * Core.Movement.FacingDirection);

        _velocityToSet = velocity;
        _shouldSetVelocity = true;
    }

    public void CheckPlayerFlip(bool value)// 確認 flip
    {
        _shouldCheckFlip = value;
    }
    
    #endregion
    
    #region w/ State Workflow

    public override void Enter()
    {
        base.Enter();

        _shouldSetVelocity = false;
        _weapon.EnterWeapon();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _weapon.LogicUpdateWeapon();
        
        if (_shouldCheckFlip)// 是否要翻轉
        {
            Core.Movement.CheckIfShouldFlip(XInput);
        }

        if (_shouldSetVelocity)// 是否能變更 velocity
        {
            Core.Movement.SetVelocityX(_velocityToSet * Core.Movement.FacingDirection);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        _weapon.ExitWeapon();
    }    

    #endregion

    #region w/ Animation Trigger Functions

    public override void AnimationFinishTrigger()// 動畫結束 => IsAbilityDone = true
    {
        base.AnimationFinishTrigger();

        IsAbilityDone = true;
    }    

    #endregion
    

}
