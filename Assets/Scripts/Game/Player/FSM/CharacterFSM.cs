using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFSM : MonoBehaviour
{

    public CharacterSettings settings;

    //state machine stuff
    public State curState;
    GameController c;
    public GameObject jumpPrefab, landPrefab, wallClingPrefab, wallJumpPrefab, dashStartPrefab, dashcontPrefab;
    public GameObject player;

    //unity objs
    Rigidbody2D rb;
    public Animator anim;
    public PhysicsMaterial2D fullFriction, noFriction;// material for staying still with infinite friction, or moving with no friction

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
        //anim = GetComponent<Animator>();

        //custom controller initialization
        movement = gameObject.AddComponent<Player_Movement>().Initialize(rb, settings.GetSettingsFor(), fullFriction, noFriction);
        animator = gameObject.AddComponent<Player_Animator>().Initialize(anim, jumpPrefab, landPrefab, wallClingPrefab, wallJumpPrefab, dashStartPrefab, dashcontPrefab, player);

        //state initialization
        states.Add(typeof(IdleState), gameObject.AddComponent<IdleState>().Initialize(this, c, movement, animator));
        states.Add(typeof(JumpState), gameObject.AddComponent<JumpState>().Initialize(this, c, movement, animator));
        states.Add(typeof(FallState), gameObject.AddComponent<FallState>().Initialize(this, c, movement, animator));
        states.Add(typeof(WalkState), gameObject.AddComponent<WalkState>().Initialize(this, c, movement, animator));
        states.Add(typeof(DashState), gameObject.AddComponent<DashState>().Initialize(this, c, movement, animator));

        states.Add(typeof(WallClingState), gameObject.AddComponent<WallClingState>().Initialize(this, c, movement, animator));
        states.Add(typeof(WallJumpState), gameObject.AddComponent<WallJumpState>().Initialize(this, c, movement, animator));

        //entry state
        ChangeState<IdleState>();

    }

    public void FixedUpdate()
    {
        curState.Loop();
    }

    public void ChangeState<T>()
    {
        Debug.Log("Changing state to: " + typeof(T).ToString());
        if (curState != null)
        {
            curState.End();
        }

        checkLanding<T>();

        states[typeof(T)].Begin();
        curState = states[typeof(T)];

    }

    //special case, animator only play the landing particles if going from FallState or JumpState, to WalkState
    //the FSM is the only place in the code that can know this scenario
    private void checkLanding<T>()
    {
        if ((curState == states[typeof(FallState)] || curState == states[typeof(JumpState)]) && typeof(T) == typeof(WalkState))
        {
            animator.createLandingParticle(Vector2.Angle(movement.getNormal(), Vector2.up));
        }
    }

    public T GetState<T>() where T : class
    {
        return states[typeof(T)] as T;
    }
}
