using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region w/ Weapon Data

    [SerializeField] protected SO_WeaponData weaponData;

    protected SO_WeaponData WeaponData
    {
        get => weaponData;
        private set => weaponData = value;
    }
    
    #endregion

    #region Components

    protected Core Core;
    protected PlayerAttackState State;

    #endregion
    
    #region w/ Variables

    protected int AttackCounter;
    
    #endregion

    #region w/ Weapon

    public void InitializeWeapon(PlayerAttackState state, Core core)// 初始
    {
        State = state;
        Core = core;
    }

    private void CheckAttackCounter()// 若已達最大攻擊次數 => 重設攻擊次數
    {
        if (AttackCounter >= WeaponData.AmountOfAttacks)
        {
            AttackCounter = 0;
        }
    }

    private void AddAttackCounter()// 增加攻擊次數
    {
        AttackCounter++;
    }
    
    #endregion
    
    #region w/ Weapon Workflow

    public virtual void EnterWeapon()
    {
        CheckAttackCounter();
    }

    public virtual void ExitWeapon()
    {
        AddAttackCounter();
    }

    public virtual void LogicUpdateWeapon() { }

    #endregion

    #region w/ Animation Trigger Functions

    public virtual void AnimationFinishTrigger()// 動畫結束 => IsAbilityDone = true
    {
        State.AnimationFinishTrigger();
    }

    public virtual void AnimationTurnOffFlipTrigger()// 是否能翻轉 = false
    {
        State.CheckPlayerFlip(false);
    }
    
    public virtual void AnimationTurnOnFlipTrigger()// 是否能翻轉 = true
    {
        State.CheckPlayerFlip(true);
    }

    public virtual void AnimationStopMovementTrigger()// 停止移動
    {
        State.SetPlayerVelocity(0f);
    }

    public virtual void AnimationStartMovementTrigger()// 根據當前的攻擊次數來變更 velocity 以移動
    {
        State.SetPlayerVelocity(WeaponData.MovementSpeed[AttackCounter]);
    }
    
    public virtual void AnimationActionTrigger() { }// 根據動作需求實現

    #endregion
}
