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

    WallCollisionType collisionType;

    private float CoyoteJumpTime = -10f;
    private float BufferJumpTime = -10f;

    public bool grounded = true;
    private bool accel = true;
    private bool hitHead = false;
    private bool stall = false;

    private bool wallCling = false;

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

        else if (vel > 0)
        {
            if (velX < 0) velX = 0; //stop on a dime when turning

            if(accel)
            {
                if(velX < c.WALK_MAX_SPEED)
                {
                    velX += vel * c.WALKING_ACCELERATION * Time.deltaTime;
                }
                else if (velX > c.WALK_MAX_SPEED)
                {
                    velX -= vel * c.WALKING_ACCELERATION * Time.deltaTime;
                }
            }
            else
            {
                velX = c.WALK_MAX_SPEED;
            }
        }
        else if (vel < 0)
        {
            if (velX > 0) velX = 0;

            if(accel)
            {

                if (velX < -1 * c.WALK_MAX_SPEED)
                {
                    velX -= vel * c.WALKING_ACCELERATION * Time.deltaTime;
                }
                else if (velX > -1 * c.WALK_MAX_SPEED)
                {
                    velX += vel * c.WALKING_ACCELERATION * Time.deltaTime;
                }

            }
            else
            {
                velX = -1 * c.WALK_MAX_SPEED;
            }
            
        }
    }

    public void UpdateFall(float vel)
    {

        if (vel > 0)
        {
            if (velX < 0) velX = 0; //stop on a dime when turning

            if (velX < c.WALK_MAX_SPEED)
            {
                velX += vel * c.WALKING_ACCELERATION * Time.deltaTime;
            }
        }
        else if (vel < 0)
        {
            if (velX > 0) velX = 0;

            if (velX > -1 * c.WALK_MAX_SPEED)
            {
                velX += vel * c.WALKING_ACCELERATION * Time.deltaTime;
            }


        }
    }

    public void ApplyMovement()
    {

        rb.velocity = new Vector2(velX, velY);

    }

    public void SetLastCoyoteJump(bool jump = true)
    {
        if (!jump)
        {
            CoyoteJumpTime = -10f;
        }
        else
        {
            CoyoteJumpTime = Time.time;
        }
        
    }

    public void Glide()
    {
        velY = -1 * c.GLIDE_SPEED;
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
        if(velY > 0 && stall)
        {
            velY *= c.JUMP_STALL;
            stall = false;
        }
    }

    public bool GetGrounded()
    {
        return grounded;
    }

    public bool GetWallCling()
    {
        return wallCling;
    }

    public void StopRisingIfHitHead()
    {
        if(hitHead && velY > 0)
        {
            velY = 0;
        }
    }

    public void SetBufferJump(bool bufferJump = true)
    {
        if(!bufferJump)
        {
            BufferJumpTime = -10f;
        }
        else
        {
            BufferJumpTime = Time.time;
        }
        

    }

    public bool CanBufferJump()
    {
        if(velY < 0 && BufferJumpTime > Time.time - c.BUFFER_JUMP_TIME)
        {
            return true;
        }
        return false;
    }

    public bool DashEnded(float dashTime)
    {
        if(Time.time > dashTime + c.DASH_DURATION)
        {
            return true;
        }
        return false;
    }

    public void SetStallJump(bool val)
    {
        stall = val;
    }

    public void Dash(bool left)
    {

        if(!left)
        {
            velX = c.DASH_SPEED;
        }
        else
        {
            velX = -1*c.DASH_SPEED;
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.contacts[0].normal.normalized == Vector2.up)
        {
            grounded = true;
        }

        if(collision.contacts[0].normal.normalized.y == 0) {

            wallCling = true;

            if(collision.contacts[0].normal.normalized == Vector2.right)
            {
                collisionType = WallCollisionType.leftWall;
            }

            else if(collision.contacts[0].normal.normalized == Vector2.left)
            {
                collisionType = WallCollisionType.rightWall;
            }

        }

        if(Vector2.Dot(collision.contacts[0].normal.normalized, Vector2.down) > 0.2f)
        {
            hitHead = true;
        }
    }

    public WallCollisionType GetWallCollisionType()
    {
        return collisionType;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        wallCling = false;
        collisionType = WallCollisionType.None;

    }


    public void Jump()
    {
        velY = c.JUMP_VEL;

        grounded = false;
        hitHead = false;

        BufferJumpTime = Time.time;
    }

    public void Fall()
    {
        velY = -0.5f;
    }

}

public enum WallCollisionType
{
    leftWall,
    rightWall,
    None
}
