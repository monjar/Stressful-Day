using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float topSpeed = 4;
    public float accelerationMag = 4f;
    public float stopAccelerationMag = 40f;
    public Rigidbody2D rBody;
    public Animator animator;

    private Vector2 _currentMovement;


    private CharacterMovementState _movementState;

    private void Update()
    {
        _currentMovement.x = Input.GetAxisRaw("Horizontal");
        _currentMovement.y = Input.GetAxisRaw("Vertical");
        _currentMovement.Normalize();
        if (_currentMovement.x < -0.01f )
        {
            _movementState = CharacterMovementState.LEFT;
            animator.SetBool("IsWalkingUp", false);
            animator.SetBool("IsWalkingDown", false);
            animator.SetBool("IsWalkingLeft", true);
            animator.SetBool("IsWalkingRight", false);
        }
        else if (_currentMovement.x > 0.01f  )
        {
            _movementState = CharacterMovementState.RIGHT;
            animator.SetBool("IsWalkingUp", false);
            animator.SetBool("IsWalkingDown", false);
            animator.SetBool("IsWalkingLeft", false);
            animator.SetBool("IsWalkingRight", true);
        }

        else if (_currentMovement.y > 0.01f )
        {
            _movementState = CharacterMovementState.UP;
            animator.SetBool("IsWalkingUp", true);
            animator.SetBool("IsWalkingDown", false);
            animator.SetBool("IsWalkingLeft", false);
            animator.SetBool("IsWalkingRight", false);
        }
        else if (_currentMovement.y < -0.01f)
        {
            _movementState = CharacterMovementState.DOWN;
            animator.SetBool("IsWalkingUp", false);
            animator.SetBool("IsWalkingDown", true);
            animator.SetBool("IsWalkingLeft", false);
            animator.SetBool("IsWalkingRight", false);
        }
        else if (Mathf.Abs(_currentMovement.y) < 0.01f  && Mathf.Abs(_currentMovement.x) < 0.01f)
        {
            _movementState = CharacterMovementState.IDLE;
            animator.SetBool("IsWalkingUp", false);
            animator.SetBool("IsWalkingDown", false);
            animator.SetBool("IsWalkingLeft", false);
            animator.SetBool("IsWalkingRight", false);
        }
        
    }


    public void ApplyAcceleration(Vector2 acc)
    {
        var acceleratedSpeed = rBody.velocity + acc;
        rBody.velocity = acceleratedSpeed.magnitude > topSpeed
            ? acceleratedSpeed.normalized * topSpeed
            : acceleratedSpeed;
    }

    public void StopInY()
    {
        var acc = (-rBody.velocity.y) * (Time.fixedDeltaTime * stopAccelerationMag);
        ApplyAcceleration(new Vector2(0, acc));
    }

    public void StopInX()
    {
        var acc = (-rBody.velocity.x) * (Time.fixedDeltaTime * stopAccelerationMag);
        ApplyAcceleration(new Vector2(acc, 0));
    }

    public void Move()
    {
        var isChangingDir = Vector2.Dot(_currentMovement, rBody.velocity) < 0;

        var acc = _currentMovement * (Time.fixedDeltaTime *
                                      (isChangingDir ? (accelerationMag + stopAccelerationMag) : accelerationMag));
        ApplyAcceleration(acc);
    }

    private void FixedUpdate()
    {
        if (!CharacterManager.Instance.IsGameGoing)
        {
            StopInX();
            StopInY();
            return;
        }

        var isStoppingInX = (Mathf.Abs(_currentMovement.x) < 0.01f);
        var isStoppingInY = (Mathf.Abs(_currentMovement.y) < 0.01f);

        if (isStoppingInX)
        {
            StopInX();
        }

        if (isStoppingInY)
        {
            StopInY();
        }

        if (!isStoppingInY || !isStoppingInX)
        {
            Move();
        }
    }

    protected enum CharacterMovementState
    {
        UP,
        IDLE,
        DOWN,
        LEFT,
        RIGHT
    }
}