using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Input singleton
    public static GameController mainController;

    public delegate void MovementInput(float vel);
    public MovementInput horizontal;
    public MovementInput vertical;

    public delegate void KeyInput(bool toggled);

    public KeyInput jump;
    public KeyInput jumpRelease;

    public KeyInput dash;


    void Awake()
    {
        mainController = this;
    }

}
