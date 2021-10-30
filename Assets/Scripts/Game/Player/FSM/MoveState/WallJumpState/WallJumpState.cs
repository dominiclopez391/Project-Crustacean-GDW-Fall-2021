using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WallJumpState : State
{

    float tWallJump = 0f;
    float WallJumpTime = 0.2f;

    public override void Begin()
    {
        animator.handleMirroring(movement.GetWallCollisionType() == WallCollisionType.rightWall);
        movement.WallJump();
        movement.Jump();
        tWallJump = 0f;
    }

    public override void Loop()
    {
        base.Loop();
        tWallJump += Time.deltaTime;
        
        if(tWallJump > WallJumpTime)
        {
            fsm.ChangeState<FallState>();
        }

        movement.UpdateGravity();
        movement.UpdateMidair();
        movement.StopRisingIfHitHead();


    }

    public override void End()
    {

    }
}
