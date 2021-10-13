using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Player_Animator : MonoBehaviour
{
    Animator anim;


    public Player_Animator Initialize(Animator anim)
    {
        this.anim = anim;
        return this;
    }


    //controller for managing animator FSM settings
    public void Walk(float vel)
    {
        anim.SetBool("isWalking", vel != 0);
    }

    public void Jump(bool jump)
    {
        anim.SetBool("isInAir", jump);
    }

    public void handleMirroring(float vel)
    {
        if (vel < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        else if (vel > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    
}
