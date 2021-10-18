using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : MoveState
{

    public override void Begin()
    {
        c.horizontal += movement.UpdateWalk;
        c.jumpRelease += Stall;
        base.Begin();
        movement.SetAccel(false);
        c.vertical += movement.FastFall;
        movement.SetBufferJump();
        movement.SetStallJump(true);
        c.jump += Jump;
    }

    public override void Loop()
    {
        base.Loop();
        movement.UpdateGravity();
        movement.Walk();
        movement.StopRisingIfHitHead();

        if(movement.GetGrounded())
        {
            fsm.ChangeState<WalkState>();
        }

        
    }

    public void Jump(bool jump)
    {
        if (jump && movement.CanBufferJump())
        {
            fsm.ChangeState<JumpState>();
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
        c.vertical -= movement.FastFall;
        c.jump -= Jump;
        base.End();
    }

}
