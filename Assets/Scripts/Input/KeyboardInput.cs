using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{

    GameController c;

    void Start()
    {
        c = GameController.mainController;
    }

    private void Update()
    {

        if(c.horizontal != null)
        {
            c.horizontal(Input.GetAxisRaw("Horizontal"));
        }

        if(c.jump != null)
        {
            c.jump(Input.GetKeyDown(KeyCode.Space));
        }
        
    }

}
