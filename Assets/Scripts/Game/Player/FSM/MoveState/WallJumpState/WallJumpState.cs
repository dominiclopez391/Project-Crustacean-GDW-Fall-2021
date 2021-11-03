using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WallJumpState : State
{
    bool dash = false;
    float tWallJump = 0f;
    float WallJumpTime = 0.1f;

    public override void Begin()
    {
        animator.handleMirroring(movement.GetWallCollisionType() == WallCollisionType.rightWall);
        animator.createWallJumpParticle();
        movement.WallJump(dash);
        movement.Jump();
        movement.SetStallJump(true);
        c.jumpRelease += Stall;
        tWallJump = 0f;
    }

    public void SetDash(bool dash)
    {
        this.dash = dash;
    }

    public override void Loop()
    {
        base.Loop();
        tWallJump += Time.deltaTime;
        movement.UpdateGravity();
        movement.UpdateMidair();
        movement.StopRisingIfHitHead();
        if (tWallJump > WallJumpTime)
        {
            fsm.GetState<FallState>().SetDash(dash);
            fsm.ChangeState<FallState>();
        }

    }

    public void SetDash(bool dash)
    {
        this.dash = dash;
    }

    public override void End()
    {
        dash = false;
        c.jumpRelease -= Stall;
    }

    public void Stall(bool release)
    {
        if (release)
        {
            movement.StallJump();
        }
    }

}
