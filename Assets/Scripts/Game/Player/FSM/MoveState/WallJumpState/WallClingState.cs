using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClingState : MoveState
{

    public WallCollisionType wcType;

    public override void Begin()
    {
        base.Begin();
        wcType = movement.GetWallCollisionType();
        c.horizontal += Cling;
        c.jump += Jump;
        
    }

    public void Cling(float horz)
    {
        if(horz != 0 
            && horz != (wcType == WallCollisionType.leftWall ? -1 : 1)
            || !movement.GetWallCling())
        {
            fsm.ChangeState<FallState>();
        }

    }

    public void Jump(bool jump)
    {
        if(jump)
        {
            fsm.ChangeState<WallJumpState>();
        }
    }

    public override void Loop()
    {
        base.Loop();
        movement.Glide();
        movement.UpdateMidair();
    }

    public override void End()
    {
        base.End();
        c.horizontal -= Cling;
        c.jump -= Jump;
    }




}

