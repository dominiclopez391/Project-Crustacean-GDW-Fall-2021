using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMoveState : EnemyState
{
    protected float direction;

    public override void Initialize()
    {
        if (en.LEFT_FIRST)
            direction = -1.0f; // to the left
        else direction = 1.0f;
    }

    public override void Begin()
    {
        
    }


    public override void Loop()
    {
        animator.handleMirroring(direction);
    }


    public override void End()
    {

    }

    public void setDirection(float d)
    {
        direction = d;
    }
}
