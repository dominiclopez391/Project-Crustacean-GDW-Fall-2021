using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{

    protected CharacterFSM fsm;
    protected GameController c;

    public virtual State Initialize(CharacterFSM fsm, GameController controller)
    {
        this.fsm = fsm;
        this.c = controller;

        return this;

    }

    public abstract void Begin();

    public virtual void Loop() { 
    

    
    } //can be empty, which is why it's virtual with empty

    public abstract void End();

}
