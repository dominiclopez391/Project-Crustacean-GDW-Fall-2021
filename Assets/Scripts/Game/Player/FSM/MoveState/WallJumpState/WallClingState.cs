using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClingState : MoveState
{

    public WallCollisionType wcType;
    public bool dash = false;

    public override void Begin()
    {
        base.Begin();
        wcType = movement.GetWallCollisionType();
        c.horizontal += Cling;
        c.jump += Jump;
        c.dash += IsDashing;
        animator.Wall(true);

    }

    public void Cling(float horz)
    {
        if(horz == (wcType == WallCollisionType.leftWall ? -1 : 1))
        {
            movement.Glide();

        }

        else
        {
            movement.UpdateGravity();
        }


        if (horz != 0
            && horz != (wcType == WallCollisionType.leftWall ? -1 : 1)
            || !movement.GetWallCling())
        {
            fsm.ChangeState<FallState>();
        }


    }

    public void IsDashing(bool dash)
    {
        this.dash = dash;
    }

    public void Jump(bool jump)
    {
        if(jump)
        {
            fsm.GetComponent<WallJumpState>().SetDash(dash);
            fsm.ChangeState<WallJumpState>();
        }
    }

    public override void Loop()
    {
        base.Loop();
        movement.UpdateMidair();
        animator.createWallClingParticle();
    }

    public override void End()
    {
        base.End();
        c.dash -= IsDashing;
        c.horizontal -= Cling;
        c.jump -= Jump;
        animator.Wall(false);
    }





}

