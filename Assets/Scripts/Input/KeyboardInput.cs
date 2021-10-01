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
        c.horizontal(Input.GetAxis("Horizontal"));
    }

}
