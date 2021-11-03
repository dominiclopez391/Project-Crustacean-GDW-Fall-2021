using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //Component references
    Rigidbody2D rb;

    WallCollisionType collisionType;

    //movement number references
    public float velX, velY;
    private float CoyoteJumpTime = -10f;
    private float BufferJumpTime = -10f;
    //Used for slope movement
    private float maxSlopeAngle = 45f;//maximum ground slope angle, in degrees
    private Vector2 slopeNormalPerp; // slope parallel to the ground collided with

    //movement state references
    public bool grounded = true;
    private bool accel = true;
    private bool hitHead = false;
    private bool stall = false;

    private bool wallCling = false;
    //movement friction references
    private PhysicsMaterial2D fullFriction, noFriction;

    //Vectors that determine if the players collision is with the ground or unwalkable walls
    private Vector2 clockwise, counterClockwise, normal;

    Character c;

    public Player_Movement Initialize(Rigidbody2D rb, Character c, PhysicsMaterial2D ff, PhysicsMaterial2D nf)
    {
        this.c = c;
        this.rb = rb;
        fullFriction = ff;
        noFriction = nf;
        return this;
    }

    public void UpdateGravity()
    {
        if (velY > c.MAX_FALL_SPEED)
        {
            velY -= c.GRAVITY_ACCELERATION * Time.deltaTime;
        }
    }

    public void SetAccel(bool accel)
    {
        this.accel = accel;
    }

    /*
     * Updates during player walking
     * Player swaps between full friction material and no friction material
     * If player is still, there is full friction
     * Otherwise player has no friction
     * 
     * During movement, player accelarates up to full walking speed
     * */
    public void UpdateWalk(float vel)
    {

        if (vel == 0)
        {
            velX = 0;
            rb.sharedMaterial = fullFriction;
        }

        else if (vel > 0)
        {
            rb.sharedMaterial = noFriction;
            if (velX < 0) velX = 0; //stop on a dime when turning

            if (accel)
            {
                if (velX < c.WALK_MAX_SPEED)
                {
                    velX += vel * c.WALKING_ACCELERATION * Time.deltaTime;
                }
                else if (velX > c.WALK_MAX_SPEED)
                {
                    velX = c.WALK_MAX_SPEED;
                }
            }
            else
            {
                velX = c.WALK_MAX_SPEED;
            }
        }
        else if (vel < 0)
        {
            rb.sharedMaterial = noFriction;
            if (velX > 0) velX = 0;

            if (accel)
            {

                if (velX < -1 * c.WALK_MAX_SPEED)
                {
                    velX = -1 * c.WALK_MAX_SPEED;
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

    public void SetFrictionless(bool frictionless)
    {
        if (frictionless)
        {
            rb.sharedMaterial = noFriction;
        }
        else
        {
            rb.sharedMaterial = fullFriction;
        }

    }
    
    /*
     * Update call during falling
     * ensures player does not have friction, and
     * allows player to move in air
     */
    public void UpdateFall(float vel)
    {
        rb.sharedMaterial = noFriction;
        if (vel == 0)
        {
            velX = 0;

        }

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

    public void UpdateMidair()
    {
        rb.velocity = new Vector2(velX, velY);
    }

    /*
     * Determines velocity of the players movement
     * If on ground, its parallel to ground
     * If in air, its the actual player velocity, left or right on x-axis
     * */
    public void Walk()
    {
        //Debug rays of collision during walk
        /*
        Debug.DrawRay(this.transform.position, normal, Color.green);
        Debug.DrawRay(this.transform.position, clockwise, Color.red);
        Debug.DrawRay(this.transform.position, counterClockwise, Color.red);
        */
        
        if (GetGrounded()) //if on ground, flat or sloped
        {
            Debug.DrawRay(this.transform.position, slopeNormalPerp, Color.yellow);
            rb.velocity = new Vector2(slopeNormalPerp.x * -velX, slopeNormalPerp.y * -velX);
        }
        else  //If in air
        {
            UpdateMidair();
        }

        //Debug.Log(velX);
        //rb.velocity = new Vector2(velX, velY);

    }

    public void WallJump(bool dash)
    {
        float speed = dash ? c.DASH_SPEED : c.WALK_MAX_SPEED;

        velX = (GetWallCollisionType() == WallCollisionType.leftWall ? speed : -1 * speed);
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
        rb.sharedMaterial = noFriction;
        velX = 0;
        if (velY > 0)
        {
            UpdateGravity();
        }
        else if (velY > -1 * c.GLIDE_SPEED)
        {
            velY += -1 * c.WALKING_ACCELERATION * Time.deltaTime;
        }
        else
        {
            velY = -1 * c.GLIDE_SPEED;
        }

    }

    public bool CanCoyoteJump()
    {

        return CoyoteJumpTime > Time.time - c.COYOTE_JUMP_TIME;
    }

    public void FastFall(float vert)
    {
        if (vert < 0)
        {
            velY = c.MAX_FALL_SPEED;
        }
    }

    public void StallJump()
    {
        if (velY > 0 && stall)
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
        if (hitHead && velY > 0)
        {
            velY = 0;
        }
    }

    public void SetBufferJump(bool bufferJump = true)
    {
        if (!bufferJump)
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
        if (velY < 0 && BufferJumpTime > Time.time - c.BUFFER_JUMP_TIME)
        {
            return true;
        }
        return false;
    }

    public bool DashEnded(float dashTime)
    {
        if (Time.time > dashTime + c.DASH_DURATION)
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

        if (!left)
        {
            velX = c.DASH_SPEED;
        }
        else
        {
            velX = -1 * c.DASH_SPEED;
        }

    }

    /**
     * Checks the normal of the collision
     * and determines if 
     * 1) the player is on ground, (grounded = true)
     * 2) hitting a ceiling (hitHead = true)
     * 3) or "other"; hitting a steep slope or wall (neither)
     * */
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.normalized.y == 0)
        {
            wallCling = true;

            if (collision.contacts[0].normal.normalized == Vector2.right)
            {
                collisionType = WallCollisionType.leftWall;
            }

            else if (collision.contacts[0].normal.normalized == Vector2.left)
            {
                collisionType = WallCollisionType.rightWall;
            }
        }


        float radMaxAngle = Mathf.Deg2Rad * maxSlopeAngle;
        clockwise = new Vector2(
            Vector2.up.x * Mathf.Cos(radMaxAngle) - Vector2.up.y * Mathf.Sin(radMaxAngle),
            Vector2.up.x * Mathf.Sin(radMaxAngle) + Vector2.up.y * Mathf.Cos(radMaxAngle)
        );
        counterClockwise = new Vector2(
            Vector2.up.x * Mathf.Cos(-radMaxAngle) - Vector2.up.y * Mathf.Sin(-radMaxAngle),
            Vector2.up.x * Mathf.Sin(-radMaxAngle) + Vector2.up.y * Mathf.Cos(-radMaxAngle)
        );
        normal = collision.contacts[0].normal.normalized;
        slopeNormalPerp = Vector2.Perpendicular(normal).normalized;

        if (Vector3.Dot(Vector3.Cross(clockwise, normal), Vector3.Cross(clockwise, counterClockwise)) >= 0
            && Vector3.Dot(Vector3.Cross(counterClockwise, normal), Vector3.Cross(counterClockwise, clockwise)) >= 0)
        //if (AxB * AxC >= 0 && CxB * CxA >= 0) //what the above line is, as an equation
        //if (collision.contacts[0].normal.normalized == Vector2.up) //old code, only defined ground as flat
        {
            grounded = true;
        }

        if (collision.contacts[0].normal.normalized.y == 0)
        {

            wallCling = true;

            if (collision.contacts[0].normal.normalized == Vector2.right)
            {
                collisionType = WallCollisionType.leftWall;
            }

            else if (collision.contacts[0].normal.normalized == Vector2.left)
            {
                collisionType = WallCollisionType.rightWall;
            }

        }

        if (Vector2.Dot(collision.contacts[0].normal.normalized, Vector2.down) > 0.2f)
        {
            hitHead = true;
        }
    }


    public WallCollisionType GetWallCollisionType()
    {
        return collisionType;
    }

    //Exiting collision must mean player left ground, if on it

    public void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        wallCling = false;

    }

    public void Jump()
    {
        velY = c.JUMP_VEL;
        Fall();
    }

    public void Fall()
    {
        BufferJumpTime = Time.time;
        grounded = false;
        hitHead = false;
    }

}

public enum WallCollisionType
{
    leftWall,
    rightWall,
    None
}
