using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region w/ Check Transform and Check Distance

    [SerializeField] private LayerMask whatIsGround;
    
    [SerializeField] private Transform groundCheck;
    
    [SerializeField] private float groundCheckRadius;

    public LayerMask WhatIsGround
    {
        get => whatIsGround;
        private set => whatIsGround = value;
    }
    
    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, Core.transform.parent.name);
        private set => groundCheck = value;
    }

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

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
    }

    #endregion
}
