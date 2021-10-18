using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : MoveState
{
    float tUngrounded = 0f;
    public override void Begin()
    {
        tUngrounded = Time.time;

        c.horizontal += movement.UpdateWalk;
        c.jumpRelease += Stall;
        c.jump += Jump;
        base.Begin();
        movement.SetAccel(false);
        c.vertical += movement.FastFall;
        movement.SetStallJump(true);
        animator.Fall(true);
    }

    public override void Loop()
    {
        base.Loop();
        movement.UpdateGravity();
        movement.Walk();
        movement.StopRisingIfHitHead();

        if(movement.GetGrounded() && tUngrounded + 0.01f < Time.time)
        {
            fsm.ChangeState<WalkState>();
            animator.Fall(false);
        }

        
    }

    public void Jump(bool jump)
    {
        if(jump)
        {
            movement.SetBufferJump();
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