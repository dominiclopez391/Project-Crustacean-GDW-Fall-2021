using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MoveState
{

    public override void Begin()
    {
        c.horizontal += movement.Walk;
        movement.Jump();
        base.Begin();
    }

    public override void Loop()
    {
        base.Loop();
        movement.UpdateGravity();

        if(movement.CheckGrounded())
        {
            fsm.ChangeState(typeof(WalkState));
        }
    }

    public override void End()
    {
        c.horizontal -= movement.Walk;
        base.End();
    }

}
