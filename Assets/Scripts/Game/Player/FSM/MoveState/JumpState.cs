using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : FallState
{
    public override void Begin()
    {
        base.Begin();
        c.jump += Jump;
        movement.Jump();
        animator.Jump(true);
        movement.SetLastCoyoteJump(false);
        movement.SetBufferJump(false);
        animator.createJumpingParticle(Vector2.Angle(movement.getNormal(), Vector2.up));
    }


    public override void End()
    {
        c.jump -= Jump;
        base.End();
    }
}
