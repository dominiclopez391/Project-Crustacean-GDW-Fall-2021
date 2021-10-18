using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : FallState
{
    public override void Begin()
    {
        base.Begin();
        movement.Jump();
        animator.Jump(true);
        movement.SetLastCoyoteJump(false);
        movement.SetBufferJump(false);
    }


    public override void End()
    {
        base.End();
    }
}
