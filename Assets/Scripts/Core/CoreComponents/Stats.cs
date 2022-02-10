using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    #region w/ Health

    [SerializeField] private float maxHealth = 100f;
    
    private float _currentHealth;

    public void DecreaseHealth(float amount)// 減少生命
    {
        _currentHealth -= amount;
        if (!(_currentHealth <= 0f)) return;
        
        _currentHealth = 0f;
        Debug.Log(" Player's Health is Zero ! ");
    }
    
    public void IncreaseHealth(float amount)// 增加生命
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
    }
    
    #endregion

    #region w/ Unity Callback Functions

    protected override void Awake()
    {
        base.Awake();

        _currentHealth = maxHealth;
    }    

    #endregion
}
