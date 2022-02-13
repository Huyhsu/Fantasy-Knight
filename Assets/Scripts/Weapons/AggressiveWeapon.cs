using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    protected SO_AggressiveWeaponData aggressiveWeaponData;

    protected override void Awake()
    {
        base.Awake();
        
        if (WeaponData.GetType() == typeof(SO_AggressiveWeaponData))
        {
            aggressiveWeaponData = (SO_AggressiveWeaponData) WeaponData;
        }
        else
        {
            Debug.Log(" Wrong Data for the Weapon ! ");
        }
    }

    #region w/ Weapon Workflow

    public override void EnterWeapon()
    {
        base.EnterWeapon();

        BaseAnimator.SetBool("isCrouching", State.IsCrouching);
    }

    public override void ExitWeapon()
    {
        base.ExitWeapon();
    }

    public override void LogicUpdateWeapon()
    {
        base.LogicUpdateWeapon();
    }

    #endregion
    
    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
    }
}
