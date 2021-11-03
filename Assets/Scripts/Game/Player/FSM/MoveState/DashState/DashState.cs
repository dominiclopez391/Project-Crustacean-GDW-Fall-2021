using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DashState : MoveState
{

    bool directionLeft = false;
    float tDash = -10f;

    public override void Initialize()
    {
        c.horizontal += SetDirection;
    }

    public void SetDirection(float horz)
    {
        if (horz == 0) return;
        this.directionLeft = horz < 0;
    }

    public override void Begin()
    {
        base.Begin();
        c.jump += Jump;
        animator.Dash(true);
        animator.createDashStartParticle();
        tDash = Time.time;

    }

    public void Jump(bool jump)
    {
        if(jump)
        {
            fsm.ChangeState<JumpState>();
        }
    }

    public override void Loop()
    {
        base.Loop();
        movement.UpdateGravity();
        movement.Dash(directionLeft);
        movement.Walk();
        animator.createDashContinueParticle();
        if (movement.DashEnded(tDash))
        {
            fsm.ChangeState<WalkState>();
        }

    }

    public override void End()
    {
        base.End();
        c.jump -= Jump;
        animator.Dash(false);
    }

}
