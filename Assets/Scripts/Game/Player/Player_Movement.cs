using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    Rigidbody2D rb;
    public float velY;

    public float GRAVITY_ACCELERATION = 10f;
    public float JUMP_VEL = 3f;
    public bool grounded = true;

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

    public void Walk(float vel)
    {
        rb.velocity = new Vector2(vel * 4, velY);

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
        velY = 3f;
    }

}
