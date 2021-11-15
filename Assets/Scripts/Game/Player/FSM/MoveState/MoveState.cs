using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveState : State
{

    public override void Begin()
    {
        
        c.horizontal += animator.handleMirroring;
    }


    public override void Loop()
    {
        
    }


    public override void End()
    {
        c.horizontal -= animator.handleMirroring;
    }

    
}
