using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFSM : MonoBehaviour
{

    //state machine stuff
    State curState;
    GameController c;

    //unity objs
    Rigidbody2D rb;
    Animator anim;

    //custom controllers
    Player_Animator animator;
    Player_Movement movement;

    private Dictionary<Type, State> states;

    void Start()
    {
        
        c = GameController.mainController;
        states = new Dictionary<Type, State>();

        //object reference initialization
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //custom controller initialization
        movement = gameObject.AddComponent<Player_Movement>().Initialize(rb);
        animator = gameObject.AddComponent<Player_Animator>().Initialize(anim);

        //state initialization
        states.Add(typeof(IdleState), gameObject.AddComponent<IdleState>().Initialize(this, c));
        states.Add(typeof(JumpState), gameObject.AddComponent<JumpState>().Initialize(this, c, movement, animator));
        states.Add(typeof(WalkState), gameObject.AddComponent<WalkState>().Initialize(this, c, movement, animator));
        

        //entry state
        ChangeState(typeof(IdleState));

    }

    private void Update()
    {
        curState.Loop();
        
    }

    public void ChangeState(Type stateType)
    {
        if(curState != null)
        {
            curState.End();
        }

        curState = states[stateType];
        curState.Begin();

    }
}
