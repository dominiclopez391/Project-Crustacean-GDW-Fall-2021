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
        c.horizontal += movement.Walk;
        c.horizontal += animator.Walk;

        c.jump += OnPlayerJump;
    }

    public override void Loop()
    {
        base.Loop();
    }

    public void OnPlayerJump(bool jump)
    {
        if(jump)
        {
            fsm.ChangeState(typeof(JumpState));
        }
    }

    public override void End()
    {
        base.End();

        c.jump -= OnPlayerJump;
        c.horizontal -= movement.Walk;
        c.horizontal -= animator.Walk;
    }

}
