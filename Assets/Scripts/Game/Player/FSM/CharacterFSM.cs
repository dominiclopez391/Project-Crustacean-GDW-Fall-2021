using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFSM : MonoBehaviour
{

    State curState;
    GameController c;
    public Animator playerAnimator;

    private Dictionary<Type, State> states;

    void Start()
    {
        c = GameController.mainController;
        states = new Dictionary<Type, State>();

        //state initialization
        states.Add(typeof(MoveState), gameObject.AddComponent<MoveState>().Initialize(this, c));


        //entry state
        ChangeState(typeof(MoveState));

    }

    private void Update()
    {
        curState.Loop();
        playerAnimator.SetBool("isWalking", Input.GetAxis("Horizontal") != 0);
    }

    void ChangeState(Type stateType)
    {
        if(curState != null)
        {
            curState.End();
        }

        curState = states[stateType];
        curState.Begin();

    }
}
