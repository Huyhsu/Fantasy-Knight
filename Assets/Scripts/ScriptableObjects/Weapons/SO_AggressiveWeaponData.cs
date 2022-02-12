using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAggressiveWeaponData", menuName = "Data/Weapon Data/Aggressive Weapon")]
public class SO_AggressiveWeaponData : SO_WeaponData
{
    #region w/ Attack Details

    [SerializeField] private WeaponAttackDetail[] attackDetails;

    public WeaponAttackDetail[] AttackDetails
    {
        get => attackDetails;
        private set => attackDetails = value;
    }    

    #endregion

    private void OnEnable()
    {
        AmountOfAttacks = AttackDetails.Length;
        MovementSpeed = new float[AmountOfAttacks];

        for (int i = 0; i < AmountOfAttacks; i++)
        {
            MovementSpeed[i] = AttackDetails[i].MovementSpeed;
        }
    }
}
