using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyWalkState : EnemyMoveState
{
    private float groundCheckDistance = 1.5f;
    public override void Begin()
    {
        base.Begin();

        movement.SetAccel(true);
        movement.SetStallJump(false);
    }

    public override void Loop()
    {
        base.Loop();
        if (movement.GetWallHit())
        {
            direction = -direction;
            //Debug.Log("TURNING : " + direction);
        }
        else if (en.WATCHES_EDGES)
        {
            int layerMask = ~LayerMask.GetMask("Enemy");
            RaycastHit2D edgewatch;
            if (direction > 0.0f)
                edgewatch = Physics2D.Raycast(gameObject.transform.position + new Vector3(GetComponent<SpriteRenderer>().bounds.size.x / 2, -GetComponent<SpriteRenderer>().bounds.size.y / 2 - 0.1f, 0), Vector2.down, groundCheckDistance);
            else edgewatch = Physics2D.Raycast(gameObject.transform.position + new Vector3(-GetComponent<SpriteRenderer>().bounds.size.x / 2, -GetComponent<SpriteRenderer>().bounds.size.y / 2 - 0.1f, 0), Vector2.down, groundCheckDistance);
            if (edgewatch.collider == null)
            {
                Debug.DrawRay((gameObject.transform.position - new Vector3(GetComponent<SpriteRenderer>().bounds.size.x / 2, GetComponent<SpriteRenderer>().bounds.size.y / 2 - 0.1f, 0)), Vector2.down, Color.red);
                direction = -direction;
                Debug.Log("Edges: " + (gameObject.transform.position));
                Debug.Log("and " + (gameObject.transform.position - new Vector3(GetComponent<SpriteRenderer>().bounds.size.x / 2, 0, 0)));
            }
            else Debug.Log(edgewatch.collider.name);
        }
        movement.UpdateWalk(direction);
        movement.Walk();
        CheckFallOff();
        if(en.IS_BOUNCER)
            Jump(true);
        
    }

    /*
    public void Dash(bool dash)
    {
        if (dash)
        {
            fsm.ChangeState<EnemyDashState>(); // imagine EnemyDashState am i right
        }
    }
    */

    public void Jump(bool jump)
    {
        if (jump || movement.CanCoyoteJump())
        {
            fsm.ChangeState<EnemyJumpState>();
        }
    }

    public void CheckFallOff()
    {
        if (!movement.GetGrounded())
        {
            fsm.GetState<EnemyFallState>().setFallSlow(true);
            fsm.GetState<EnemyFallState>().setDirection(direction);
            fsm.ChangeState<EnemyFallState>();
        }
    }


    public override void End()
    {
        base.End();

    }

}
