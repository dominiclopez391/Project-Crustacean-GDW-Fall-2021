using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    private const int PLAYER_LAYER = 3, ENEMY_LAYER = 6;
    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.IgnoreLayerCollision(ENEMY_LAYER, PLAYER_LAYER, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Upon collision with another GameObject, this GameObject use OnTriggerEnter
    //If that GameObject is the player, the player will be damaged
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Enemy collided with " + col.name); 
        
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("OWWIE");
            col.gameObject.GetComponent<Player_Combat>().takeDamage(5);
        }
    }
}
