using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour, ILogicUpdate
{
    protected Core Core;
    protected string CoreParentName;

    protected virtual void Awake()
    {
        Core = transform.parent.GetComponent<Core>();
        CoreParentName = transform.parent.name;
        
        if (Core == null)
        {
            Debug.LogError(" There is no Core on the parent ! ");
        }
        
        Core.AddComponent(this);
    }

    public virtual void LogicUpdate() { }
}
