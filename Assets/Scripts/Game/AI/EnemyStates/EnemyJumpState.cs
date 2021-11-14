using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpState : EnemyFallState
{
    public override void Begin()
    {
        base.Begin();
        //c.jump += Jump;
        movement.Jump();
        animator.Jump(true);
        movement.SetLastCoyoteJump(false);
        movement.SetBufferJump(false);
    }


    public override void End()
    {
        //c.jump -= Jump;
        base.End();
    }
}
