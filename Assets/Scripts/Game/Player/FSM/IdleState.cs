using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class IdleState : State
{
    public override void Begin()
    {
        c.horizontal += OnPlayerMove;
        c.jump += CoyoteJump;
    }


    public void OnPlayerMove(float vel)
    {
        if(vel != 0)
        {
            fsm.ChangeState(typeof(WalkState));
        }
    }

    public override void End()
    {
        c.horizontal -= OnPlayerMove;
        c.jump -= CoyoteJump;
    }

    private void CoyoteJump(bool jump)
    {
        if (jump)
        {
            movement.SetLastCoyoteJump();
        }
    }
}
