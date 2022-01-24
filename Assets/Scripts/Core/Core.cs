using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    #region w/ Core Componets
    
    private List<ILogicUpdate> _components = new List<ILogicUpdate>();
    
    private Movement _movement;
    private CollisionSenses _collisionSenses;

    public Movement Movement
    {
        get => GenericNotImplementedError<Movement>.TryGet(_movement, transform.parent.name);
        private set => _movement = value;
    }

    public CollisionSenses CollisionSenses
    {
        get => GenericNotImplementedError<CollisionSenses>.TryGet(_collisionSenses, transform.parent.name);
        private set => _collisionSenses = value;
    }
    
    #endregion

    #region w/ Unity Callback Functions

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
    }    

    #endregion

    public void LogicUpdate()
    {
        foreach (var component in _components)
        {
            component.LogicUpdate();
        }
    }

    public void AddComponent(ILogicUpdate component)
    {
        if (!_components.Contains(component))
        {
            _components.Add(component);
        }
    }
}
