using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : MoveState
{
    float tUngrounded = 0f;
    public override void Begin()
    {
        
        c.horizontal += movement.UpdateFall;
        c.jumpRelease += Stall;
        c.jump += Jump;
        
        movement.SetAccel(false);
        c.vertical += movement.FastFall;
        movement.SetStallJump(true);
        movement.SetBufferJump();
        animator.Fall(true);
        

        base.Begin();
    }

    public override void Loop()
    {
        base.Loop();
        DoPhysics();
        CheckWallCling();

        
        if(movement.GetGrounded())
        {
            fsm.ChangeState<WalkState>();
            animator.Fall(false);
        }
        
        
    }

    public void DoPhysics()
    {
        movement.UpdateGravity();
        movement.ApplyMovement();
        movement.StopRisingIfHitHead();
    }

    public void CheckWallCling()
    {
        if(movement.GetWallCling())
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
        
        c.horizontal -= movement.UpdateWalk;
        c.jumpRelease -= Stall;
        c.vertical -= movement.FastFall;
        c.jump -= Jump;
        
        base.End();
    }

}