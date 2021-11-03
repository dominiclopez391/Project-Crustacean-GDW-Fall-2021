using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : MoveState
{
    float tUngrounded = 0f;
    bool dash, fallSlow;
    public override void Begin()
    {
        if (!dash)
            c.horizontal += movement.UpdateFall;

        c.jumpRelease += Stall;
        c.jump += Jump;
        
        movement.SetAccel(false);

        
        c.vertical += movement.FastFall;
        c.horizontal += CheckWallCling;
        movement.SetStallJump(true);
        movement.SetBufferJump();
        movement.Fall(fallSlow);
        animator.Fall(true);

        tUngrounded = Time.time;

        movement.SetFrictionless(true);
        base.Begin();
    }

    public void setFallSlow(bool slow)
    {
        this.fallSlow = slow;
    }

    public virtual void SetDash(bool dash)
    {
        this.dash = dash;
    }

    public override void Loop()
    {
        base.Loop();
        DoPhysics();

        
        if(movement.GetGrounded()
            && tUngrounded + 0.1f < Time.time)
        {
            fsm.ChangeState<WalkState>();
            animator.Fall(false);
        }
        
        
    }

    public void DoPhysics()
    {
        movement.UpdateGravity();
        movement.UpdateMidair();
        movement.StopRisingIfHitHead();
    }

    public void CheckWallCling(float horz)
    {
        if(movement.GetWallCling() && horz == (movement.GetWallCollisionType() == WallCollisionType.leftWall ? -1 : 1 ))
        {
            fsm.ChangeState<WallClingState>();
        }
    }

    public void Jump(bool jump)
    {
        if(jump)
        {

            if(movement.CanBufferJump())
            {
                fsm.GetState<JumpState>().SetDash(dash);
                fsm.ChangeState<JumpState>();
            }

            movement.SetLastCoyoteJump();

        }
        
    }
        
    public void JumpRelease(bool jumpRelease)
    {
        if(jumpRelease)
        {
            movement.SetLastCoyoteJump(false);
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
        fallSlow = false;
        if (!dash)
            c.horizontal -= movement.UpdateFall;
        c.horizontal -= CheckWallCling;
        c.horizontal -= movement.UpdateWalk;
        c.jumpRelease -= Stall;
        c.vertical -= movement.FastFall;
        c.jump -= Jump;
        dash = false;
        base.End();
    }

}