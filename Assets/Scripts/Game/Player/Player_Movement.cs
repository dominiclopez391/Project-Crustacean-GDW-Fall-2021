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

    public float WALKING_ACCELERATION = 8f;
    public float GRAVITY_ACCELERATION = 14f;

    public float COYOTE_JUMP_TIME = 0.1f;

    public float JUMP_VEL = 6f; //jump height
    public float JUMP_STALL = 0.65f;

    private float CoyoteJumpTime = -10f;
    private bool grounded = true;

    public Player_Movement Initialize(Rigidbody2D rb)
    {
        this.rb = rb;
        return this;
    }

    public void UpdateGravity()
    {
        if(velY > -20f)
        {
            velY -= GRAVITY_ACCELERATION * Time.deltaTime;
        }
    }

    public void UpdateWalk(float vel)
    {

        if(vel == 0)
        {
            velX = 0;
        }

        else if (vel > 0 && velX < vel * 4)
        {
            if (velX < 0) velX = 0; //stop on a dime when turning
            velX += vel * WALKING_ACCELERATION * Time.deltaTime;
        }
        else if (vel < 0 && velX > vel * 4)
        {
            if (velX > 0) velX = 0;
            velX += vel * WALKING_ACCELERATION * Time.deltaTime;
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
        return CoyoteJumpTime > Time.time - COYOTE_JUMP_TIME;
    }

    public void StallJump()
    {
        if(velY > 0)
        {
            velY *= JUMP_STALL;
        }
    }

    public bool CheckGrounded()
    {
        return grounded;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //todo: only check for platforms
        Debug.Log(collision.contacts[0].normal.normalized);

        if (collision.contacts[0].normal.normalized == Vector2.up)
        {
            grounded = true;
        }
    }


    public void Jump()
    {
        grounded = false;
        velY = JUMP_VEL;
    }

}
