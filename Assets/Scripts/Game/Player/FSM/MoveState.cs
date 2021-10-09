using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{

    Rigidbody2D rb;

    public override State Initialize(CharacterFSM fsm, GameController controller)
    {
        
        rb = GetComponent<Rigidbody2D>();

        return base.Initialize(fsm, controller);
    }

    public override void Begin()
    {
        c.horizontal += PlayerMovement;
    }


    public void PlayerMovement(float vel)
    {
        rb.velocity = new Vector2(vel * 4, -2);
        fsm.playerAnimator.SetBool("isWalking", vel != 0);

        if(vel < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        else if(vel > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public override void Loop()
    {

        //todo: if c.jump switch to jumpstate
    }

    public override void End()
    {
        c.horizontal -= PlayerMovement;
    }

    
}
