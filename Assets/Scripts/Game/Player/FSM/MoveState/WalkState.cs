using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class WalkState : MoveState
{

    public override void Begin()
    {
        base.Begin();
        c.horizontal += animator.Walk;
        c.horizontal += movement.UpdateWalk;
        c.jump += Jump;
        c.dash += Dash;

        movement.SetAccel(true);
        movement.SetStallJump(false);
        
    }

    public override void Loop()
    {
        base.Loop();
        movement.Walk();
        CheckFallOff();
        
    }

    public void Dash(bool dash)
    {
        if(dash)
        {
            fsm.ChangeState<DashState>();
        }
    }

    public void Jump(bool jump)
    {
        if (jump || movement.CanCoyoteJump())
        {
            fsm.ChangeState<JumpState>();
        }
    }

    public void CheckFallOff()
    {
        if(!movement.GetGrounded())
        {
            fsm.ChangeState<FallState>();
        }
    }


    public override void End()
    {
        base.End();
        c.jump -= Jump;
        c.horizontal -= movement.UpdateWalk;
        c.horizontal -= animator.Walk;
        c.dash -= Dash;
    }

}
