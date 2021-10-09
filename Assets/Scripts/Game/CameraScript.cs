/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CameraScript manages the position of the camera during gameplay
 * It has 3 objects as parameters to set in unity:
 * 
 * focusObject: The object the camera tries to center, intended to be the player
 * leftBottomObject: an object whose x and y positions define the left and bottom boundaries of the edges of the camera
 * rightTopObject: an object whose x and y positions define the right and top boundaries of the edges of the camera
 * 
 * Used a modified version of the answer to this question:
 * https://answers.unity.com/questions/501893/calculating-2d-camera-bounds.html
 * The answer above is on a known area, but this game isn't, so the min and max boundries are defined dynamically with 2 corner objects
 

public class CameraScript : MonoBehaviour
{
    public GameObject focusObject;
    public GameObject leftBottomObject; // left and lower border
    public GameObject rightTopObject; //  right and upper border
    float vertExtent; //vertical extent/height of camera
    float horzExtent; //horizontal extent/width of camera

    // Use this for initialization
    void Start()
    {
        vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;
        horzExtent = vertExtent * Screen.width / Screen.height;
    }
    // Update is called once per frame
    void Update()
    {
        //Follows player as the center of the camera, inside clamped boundries
        transform.position = new Vector3(Mathf.Clamp(focusObject.transform.position.x, leftBottomObject.transform.position.x + horzExtent, rightTopObject.transform.position.x - horzExtent)
            , Mathf.Clamp(focusObject.transform.position.y, leftBottomObject.transform.position.y + vertExtent, rightTopObject.transform.position.y - vertExtent), -10);

    }
}*/
