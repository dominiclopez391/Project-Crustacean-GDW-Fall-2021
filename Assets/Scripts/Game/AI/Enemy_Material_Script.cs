using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Material_Script : MonoBehaviour
{
    const int hitDamage = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Upon collision with another GameObject, this GameObject use OnTriggerEnter
    //If that GameObject is the player, the player will be damaged
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player_Combat>().takeDamage(hitDamage);
        }
    }
}
