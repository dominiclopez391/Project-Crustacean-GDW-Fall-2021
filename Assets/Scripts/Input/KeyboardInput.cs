using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{

    GameController c;
    private bool LeftLastPressed = false;

    void Start()
    {
        c = GameController.mainController;
    }

    private void Update()
    {

        float horz = GetPriorityBasedInput();
        float vert = Input.GetAxisRaw("Vertical");

        if(c.horizontal != null)
        {
            c.horizontal(horz);
        }

        if (c.vertical != null)
        {
            c.vertical(vert);
        }

        if (c.jump != null)
        {
            c.jump(Input.GetKeyDown(KeyCode.Space));
        }

        if(c.jumpRelease != null)
        {
            c.jumpRelease(!Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.Space));
        }
        if(c.dash != null)
        {
            c.dash(Input.GetKey(KeyCode.LeftShift));
        }


        
    }

    private float GetPriorityBasedInput()
    {
        //when both keys are pressed, priority is given to the one pressed last

        if (Input.GetButtonDown("Move Left"))
        {
            LeftLastPressed = true;
        }
        else if (Input.GetButtonDown("Move Right"))
        {
            LeftLastPressed = false;
        }

        float input = 0;

        //special key input
        if (Input.GetButton("Move Left") && Input.GetButton("Move Right"))
        {
            if (LeftLastPressed)
            {
                input = -1;
            }
            else
            {
                input = 1;
            }
        }
        else if (Input.GetButton("Move Left"))
        {
            input = -1;
        }
        else if (Input.GetButton("Move Right"))
        {
            input = 1;
        }

        return input;
    }

}
