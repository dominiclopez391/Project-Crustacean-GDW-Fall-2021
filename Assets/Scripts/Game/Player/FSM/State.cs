using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected CharacterFSM fsm;
    protected GameController c;
    protected Player_Animator animator;
    protected Player_Movement movement;

    public virtual State Initialize(CharacterFSM fsm, GameController controller, Player_Movement movement, Player_Animator animator)
    {

        this.fsm = fsm;
        this.c = controller;

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
    public virtual void Loop() { 
    

    
    } //can be empty, which is why it's virtual with empty

    //called when the state is inactivated
    public abstract void End();

}
