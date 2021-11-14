using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{

    public EnemySettings settings;
    Enemy en;

    //state machine stuff
    public EnemyState curState;
    //particles for this enemy
    //public GameObject jumpPrefab, landPrefab, wallClingPrefab, wallJumpPrefab, dashStartPrefab, dashcontPrefab;
    public GameObject enemy;

    //unity objs
    Rigidbody2D rb;
    public Animator anim;
    public PhysicsMaterial2D fullFriction, noFriction; // material for staying still with infinite friction, or moving with no friction

    //custom controllers
    Enemy_Animator animator;
    Enemy_Movement movement;

    private Dictionary<Type, EnemyState> states;

    void Start()
    {
        en = settings.GetSettingsFor();
        states = new Dictionary<Type, EnemyState>();

        //object reference initialization
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();

        //custom controller initialization
        movement = gameObject.AddComponent<Enemy_Movement>().Initialize(rb, en, fullFriction, noFriction);
        animator = gameObject.AddComponent<Enemy_Animator>().Initialize(anim, enemy);

        //state initialization
        states.Add(typeof(EnemyWalkState), gameObject.AddComponent<EnemyWalkState>().Initialize(this, en, movement, animator));
        states.Add(typeof(EnemyJumpState), gameObject.AddComponent<EnemyJumpState>().Initialize(this, en, movement, animator));
        states.Add(typeof(EnemyFallState), gameObject.AddComponent<EnemyFallState>().Initialize(this, en, movement, animator));
        states.Add(typeof(EnemyStunState), gameObject.AddComponent<EnemyStunState>().Initialize(this, en, movement, animator));

        //entry state
        ChangeState<EnemyFallState>();

    }

    public void FixedUpdate()
    {
        curState.Loop();
    }

    public void ChangeState<T>()
    {
        //Debug.Log("Changing state to: " + typeof(T).ToString());
        if (curState != null)
        {
            curState.End();
        }

        states[typeof(T)].Begin();
        curState = states[typeof(T)];

    }

    public T GetState<T>() where T : class
    {
        return states[typeof(T)] as T;
    }
}
