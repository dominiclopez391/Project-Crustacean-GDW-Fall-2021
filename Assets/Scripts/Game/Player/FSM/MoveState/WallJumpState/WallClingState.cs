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
        movement.Glide();
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

    public override void Loop()
    {
        base.Loop();
        movement.ApplyMovement();
    }

    public override void End()
    {
        base.End();
        c.horizontal -= Cling;
    }




}

