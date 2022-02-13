using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    private Weapon _weapon;

    private void Start()
    {
        _weapon = GetComponentInParent<Weapon>();
    }

    private void AnimationFinishTrigger()// IsAbilityDone = true
    {
        _weapon.AnimationFinishTrigger();
    }

    private void AnimationStartMovementTrigger()// Start Movement
    {
        _weapon.AnimationStartMovementTrigger();
    }

    private void AnimationStopMovementTrigger()// Stop Movement
    {
        _weapon.AnimationStopMovementTrigger();
    }

    private void AnimationTurnOffFlipTrigger()// Flip off
    {
        _weapon.AnimationTurnOffFlipTrigger();
    }

    private void AnimationTurnOnFlipTrigger()// Flip on
    {
        _weapon.AnimationTurnOnFlipTrigger();
    }

    private void AnimationActionTrigger()// Doing
    {
        _weapon.AnimationActionTrigger();
    }

}
