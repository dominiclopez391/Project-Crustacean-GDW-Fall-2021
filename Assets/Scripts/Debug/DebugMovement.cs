using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMovement : MonoBehaviour
{

    GameController c;
    //testing
    private void Start()
    {
        c = GameController.mainController;
        c.horizontal += Test;
    }

    float time;

    public void Test(float input)
    {
        time += Time.deltaTime;

        if (time > 0.25f)
        {
            time = 0;
            Debug.Log(input);
        }

    }

}
