using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    protected SO_AggressiveWeaponData aggressiveWeaponData;

    protected virtual void Awake()
    {
        if (WeaponData.GetType() == typeof(SO_AggressiveWeaponData))
        {
            aggressiveWeaponData = (SO_AggressiveWeaponData) WeaponData;
        }
        else
        {
            Debug.Log(" Wrong Data for the Weapon ! ");
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
    }
}
