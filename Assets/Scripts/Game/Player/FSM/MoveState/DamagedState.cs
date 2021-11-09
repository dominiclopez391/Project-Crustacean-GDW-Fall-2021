using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedState : State
{
    private float damageLockTime = 2.0f;
    private float tLock;

    public override void Begin()
    {
        tLock = 0.0f;
        animator.Damage(true);
        movement.Freeze();
    }

    public override void Loop()
    {
        base.Loop();
        tLock += Time.deltaTime;
        if(damageLockTime <= tLock)
        {
            fsm.ChangeState<WalkState>();
        }
    }


    public override void End()
    {
        animator.Damage(false);
        movement.Gravity(true);
    }
}
