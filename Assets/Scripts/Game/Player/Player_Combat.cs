using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Manages the players combat variables,
 * and physical and visual effects of combat on the player
 * @Date 11/08/2021
 * @Author Evren Keskin
 * */
public class Player_Combat : MonoBehaviour
{
    public const int PLAYER_MAX_HEALTH = 20;
    int playerHealth = PLAYER_MAX_HEALTH;
    const float GRACE_PERIOD = 3.0f;
    public GameObject respawnLocation;
    public CharacterFSM fsm;
    float tGraced = GRACE_PERIOD;

    //variables for managing flashing effect
    public GameObject playerSprite;
    const float FLASH_PERIOD = 0.1f;
    float tFlash = 0.0f;
    bool isFlashed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the grace period time, and flashing time
        if (tGraced < GRACE_PERIOD && fsm.GetState<DamagedState>())
        {
            //effects during the grace period
            tGraced += Time.deltaTime;
            if (tFlash < FLASH_PERIOD)
                tFlash += Time.deltaTime;
            else if (isFlashed)
            {
                playerSprite.GetComponent<SpriteRenderer>().color = new Color(255,255,0);
                isFlashed = false;
                tFlash = 0.0f;
            }
            else
            {
                playerSprite.GetComponent<SpriteRenderer>().color = Color.white;
                isFlashed = true;
                tFlash = 0.0f;
            }
        }
        //otherwise ensure player sprite is normal
        else playerSprite.GetComponent<SpriteRenderer>().color = Color.white;
    }

    /*
     * Hurts the player if the damage call is after the grace period
     * Player state machine goes to DamagedState
     * */
    public void takeDamage(int damage)
    {
        if (tGraced >= GRACE_PERIOD)
        {
            tGraced = 0;
            fsm.ChangeState<DamagedState>();
            playerHealth -= damage;
            if (playerHealth <= 0)
                killPlayer();
        }
    }

    /*
     * Kills the player: respawn, resets to WalkState
     * and resets the grace timer
     * */
    void killPlayer()
    {
        Debug.Log("MEMENTO MORI");
        respawnPlayer();
        fsm.ChangeState<WalkState>();
        tGraced = GRACE_PERIOD;
    }

    /*
     * Respawns player with PLAYER_MAX_HEALTH 
     * and the attached respawn location
     * */
    void respawnPlayer()
    {
        playerHealth = PLAYER_MAX_HEALTH;
        this.transform.position = respawnLocation.transform.position;
    }
}
