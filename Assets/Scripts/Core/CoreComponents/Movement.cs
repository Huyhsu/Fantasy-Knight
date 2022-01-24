using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Movement : CoreComponent
{
    public Rigidbody2D Rigidbody { get; private set; }
    
    public int FacingDirection { get; private set; }
    
    public bool CanSetVelocity { get; private set; }
    
    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 _workspace;

    protected override void Awake()
    {
        base.Awake();
        Rigidbody = GetComponentInParent<Rigidbody2D>();
        FacingDirection = 1;
        CanSetVelocity = true;
    }

    public override void LogicUpdate()
    {
        CurrentVelocity = Rigidbody.velocity;
    }

    #region w/ Flip Functions

    private void Flip()
    {
        FacingDirection *= -1;
        Rigidbody.transform.Rotate(0.0f,180.0f,0.0f);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    
    #endregion
    
    #region w/ Set Velocity Functions

    public void SetVelocityZero()
    {
        _workspace = Vector2.zero;
    }

    public void SetVelocityX(float velocityX)
    {
        _workspace.Set(velocityX, CurrentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocityY)
    {
        _workspace.Set(CurrentVelocity.x, velocityY);
        SetFinalVelocity();
    }
    
    public void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            Rigidbody.velocity = _workspace;
            CurrentVelocity = _workspace;
        }
    }
    
    #endregion
}
