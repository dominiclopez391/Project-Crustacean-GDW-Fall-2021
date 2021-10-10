using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveState : State
{

    public Player_Movement movement;
    public Player_Animator animator;

    public State Initialize(CharacterFSM fsm, GameController controller, Player_Movement movement, Player_Animator animator)
    {

        this.movement = movement;
        this.animator = animator;

        return base.Initialize(fsm, controller);
    }

    public override void Begin()
    {
        
        c.horizontal += animator.handleMirroring;
    }


    public override void Loop()
    {

        //todo: if c.jump switch to jumpstate
    }

    public override void End()
    {
        c.horizontal -= animator.handleMirroring;
    }

    
}
