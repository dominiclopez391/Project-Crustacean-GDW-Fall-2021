using System;
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
    private bool grounded = true;
    private bool accel = true;

    Character c;

    public Player_Movement Initialize(Rigidbody2D rb, Character c)
    {
        this.c = c;
        this.rb = rb;
        return this;
    }

    public void UpdateGravity()
    {
        if(velY > -20f)
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

    public void StallJump()
    {
        if(velY > 0)
        {
            velY *= c.JUMP_STALL;
        }
    }

    public bool CheckGrounded()
    {
        return grounded;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.contacts[0].normal.normalized == Vector2.up)
        {
            grounded = true;
        }
    }


    public void Jump()
    {
        grounded = false;
        velY = c.JUMP_VEL;
    }

}
