﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    Rigidbody2D rb;
    public float velX, velY;

    private float CoyoteJumpTime = -10f;
    private float BufferJumpTime = -10f;

    private bool grounded = true;
    private bool accel = true;
    private bool hitHead = true;


    Character c;

    public Player_Movement Initialize(Rigidbody2D rb, Character c)
    {
        this.c = c;
        this.rb = rb;
        return this;
    }

    public void UpdateGravity()
    {
        if(velY > c.MAX_FALL_SPEED)
        {
            velY -= c.GRAVITY_ACCELERATION * Time.deltaTime;
        }
    }

    public void SetAccel(bool accel)
    {
        this.accel = accel;
    }

    public void UpdateWalk(float vel)
    {

        if(vel == 0)
        {
            velX = 0;
        }

        else if (vel > 0 && velX < c.WALK_MAX_SPEED)
        {
            if (velX < 0) velX = 0; //stop on a dime when turning

            if(accel)
            {
                velX += vel * c.WALKING_ACCELERATION * Time.deltaTime;
            }
            else
            {
                velX = c.WALK_MAX_SPEED;
            }
        }
        else if (vel < 0 && velX > -1* c.WALK_MAX_SPEED)
        {
            if (velX > 0) velX = 0;

            if(accel)
            {
                velX += vel * c.WALKING_ACCELERATION * Time.deltaTime;
            }
            else
            {
                velX = -1 * c.WALK_MAX_SPEED;
            }
            
        }
    }

    public void Walk()
    {

        rb.velocity = new Vector2(velX, velY);

    }

    public void SetLastCoyoteJump()
    {
        CoyoteJumpTime = Time.time;
    }

    public bool CanCoyoteJump()
    {
        return CoyoteJumpTime > Time.time - c.COYOTE_JUMP_TIME;
    }

    public void FastFall(float vert)
    {
        if(vert < 0)
        {
            velY = c.MAX_FALL_SPEED;
        }
    }

    public void StallJump()
    {
        if(velY > 0)
        {
            velY *= c.JUMP_STALL;
        }
    }

    public bool GetGrounded()
    {
        return grounded;
    }

    public void StopRisingIfHitHead()
    {
        if(hitHead && velY > 0)
        {
            velY = 0;
        }
    }

    public void SetBufferJump()
    {
        BufferJumpTime = Time.time;

    }

    public bool CanBufferJump()
    {
        if(velY < 0 && BufferJumpTime > Time.time - c.BUFFER_JUMP_TIME)
        {
            return true;
        }
        return false;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.contacts[0].normal.normalized == Vector2.up)
        {
            grounded = true;
        }

        if(Vector2.Dot(collision.contacts[0].normal.normalized, Vector2.down) > 0.2f)
        {
            hitHead = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.contactCount == 0)
        {
            //todo: specify only platform layers
            if(!collision.otherCollider.IsTouchingLayers())
            {
                grounded = false;
                hitHead = false;

                BufferJumpTime = Time.time;
            }
        }
    }


    public void Jump()
    {
        grounded = false;
        velY = c.JUMP_VEL;
    }

    public void Fall()
    {
        grounded = false;
        velY = -0.1f;
    }

}
