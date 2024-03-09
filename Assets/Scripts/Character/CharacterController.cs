using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float topSpeed = 4;
    public float accelerationMag = 4f;
    public float stopAccelerationMag = 40f;
    public Rigidbody2D rBody;
    public Animator animator;

    private Vector2 _currentMovement;


    private void Update()
    {
        _currentMovement.x = Input.GetAxisRaw("Horizontal");
        _currentMovement.y = Input.GetAxisRaw("Vertical");
        _currentMovement.Normalize();
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
        if(!isStoppingInY || !isStoppingInX)
        {
            Move();
        }
    }
}