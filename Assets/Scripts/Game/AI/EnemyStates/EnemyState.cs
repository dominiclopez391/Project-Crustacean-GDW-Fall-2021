using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    protected EnemyFSM fsm;
    protected Enemy en;
    protected Enemy_Animator animator;
    protected Enemy_Movement movement;

    public virtual EnemyState Initialize(EnemyFSM fsm, Enemy en, Enemy_Movement movement, Enemy_Animator animator)
    {

        this.fsm = fsm;
        this.en = en;
        this.movement = movement;
        this.animator = animator;

        Initialize();

        return this;

    }
    //creates the state
    public virtual void Initialize()
    {

    }
    //called when the state is activated
    public abstract void Begin();

    //called every frame during activity
    public virtual void Loop()
    {



    } //can be empty, which is why it's virtual with empty

    //called when the state is inactivated
    public abstract void End();

}
