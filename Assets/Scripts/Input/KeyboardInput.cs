using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{

    GameController c;
    float HorzMovement = 0;
    bool LeftLastPressed = false;

    void Start()
    {
        c = GameController.mainController;
    }

    private void Update()
    {

        if(Input.GetButtonDown("Move Left"))
        {
            LeftLastPressed = true;
        }
        else if(Input.GetButtonDown("Move Right"))
        {
            LeftLastPressed = false;
        }

        float input = 0;
        if(Input.GetButton("Move Left") && Input.GetButton("Move Right"))
        {
            if(LeftLastPressed)
            {
                input = -1;
            } else
            {
                input = 1;
            }
        }
        else if(Input.GetButton("Move Left"))
        {
            input = -1;
        }
        else if (Input.GetButton("Move Right"))
        {
            input = 1;
        }


        if(c.horizontal != null)
        {
            c.horizontal(input);
        }

        if(c.jump != null)
        {
            c.jump(Input.GetKeyDown(KeyCode.Space));
        }
        
    }

}
