using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MoveState
{

    public override void Begin()
    {
        c.horizontal += movement.UpdateWalk;
        movement.Jump();
        c.jumpRelease += Stall;
        base.Begin();
    }

    public override void Loop()
    {
        base.Loop();
        movement.UpdateGravity();
        movement.Walk();

        if(movement.CheckGrounded())
        {
            fsm.ChangeState(typeof(WalkState));
        }
    }

    public void Stall(bool release)
    {
        if(release)
        {
            movement.StallJump();
        }
    }

    public override void End()
    {
        c.horizontal -= movement.UpdateWalk;
        c.jumpRelease -= Stall;
        base.End();
    }

}
