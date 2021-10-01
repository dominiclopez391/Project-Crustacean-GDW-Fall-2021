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
        rb.velocity = new Vector2(vel * 10, -2);
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
