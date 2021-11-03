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
        c.dash += StopDash;

        movement.SetFrictionless(true);
        animator.Dash(true);
        animator.createDashStartParticle();
        tDash = Time.time;

    }

    public void Jump(bool jump)
    {
        if(jump)
        {
            fsm.GetState<JumpState>().SetDash(true);
            fsm.ChangeState<JumpState>();
        }
    }

    public override void Loop()
    {
        base.Loop();
        Debug.Log(directionLeft);

        movement.Dash(directionLeft);
        movement.UpdateGravity();
        movement.Walk();
        animator.createDashContinueParticle();
        CheckFallOff();

    }

    public void CheckFallOff()
    {
        if (!movement.GetGrounded())
        {
            fsm.GetState<FallState>().setFallSlow(true);
            fsm.GetState<FallState>().SetDash(true);
            fsm.ChangeState<FallState>();
        }
    }

    public void StopDash(bool dash)
    {
        if(!dash)
        {
            fsm.ChangeState<WalkState>();
        }
    }

    public override void End()
    {
        base.End();
        c.jump -= Jump;
        c.dash -= StopDash;
        animator.Dash(false);
    }

}
