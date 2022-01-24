using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour, ILogicUpdate
{
    protected Core Core;

    protected void Awake()
    {
        Core = transform.parent.GetComponent<Core>();

        if (!Core)
        {
            Debug.LogError(" There is no Core on the parent ! ");
        }
        
        Core.AddComponent(this);
    }

    public virtual void LogicUpdate() { }
}
