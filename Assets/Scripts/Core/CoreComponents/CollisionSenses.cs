using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region w/ Check Transform and Check Distance

    // LayerMask
    [SerializeField] private LayerMask whatIsGround;
    // Transform
    [SerializeField] private Transform groundCheck;
    // Distance
    [SerializeField] private float groundCheckRadius;

    // LayerMask
    public LayerMask WhatIsGround
    {
        get => whatIsGround;
        private set => whatIsGround = value;
    }
    // Transform
    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, Core.transform.parent.name);
        private set => groundCheck = value;
    }

    // Distance
    public float GroundCheckRadius
    {
        get => groundCheckRadius;
        private set => groundCheckRadius = value;
    }

    #endregion

    #region w/ Check Bool

    public bool Ground => Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, WhatIsGround);

    #endregion

    #region w/ Draw On Scene

    public virtual void OnDrawGizmos()
    {
        if (Core == null) return;
        
        Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
    }

    #endregion
}
