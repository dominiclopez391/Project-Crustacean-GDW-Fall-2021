﻿using System;
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
        c.jump += OnPlayerJump;
        c.horizontal += animator.Walk;
        c.horizontal += movement.UpdateWalk;
        
    }

    public override void Loop()
    {
        base.Loop();
        movement.Walk();
    }

    public void OnPlayerJump(bool jump)
    {
        if(jump || movement.CanCoyoteJump())
        {
            fsm.ChangeState(typeof(JumpState));
        }
    }

    public override void End()
    {
        base.End();
        c.jump -= OnPlayerJump;
        c.horizontal -= movement.UpdateWalk;
        c.horizontal -= animator.Walk;
    }

}
