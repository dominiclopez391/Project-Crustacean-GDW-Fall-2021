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
    public CharacterSettings settings;
    Character c;
    int playerHealth;
    public GameObject respawnLocation;
    public CharacterFSM fsm;
    float tGraced;

    //variables for managing flashing effect
    public GameObject playerSprite;
    float tFlash;
    bool isFlashed;

    // Start is called before the first frame update
    void Start()
    {
        c = settings.GetSettingsFor();
        Physics2D.IgnoreLayerCollision(c.ENEMY_LAYER, c.PLAYER_LAYER, true);
        playerHealth = c.PLAYER_MAX_HEALTH;
        tGraced = c.GRACE_PERIOD;
        tFlash = 0.0f;
        isFlashed = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the grace period time, and flashing time
        if (tGraced < c.GRACE_PERIOD && fsm.GetState<DamagedState>())
        {
            //effects during the grace period
            tGraced += Time.deltaTime;
            if (tFlash < c.FLASH_PERIOD)
                tFlash += Time.deltaTime;
            else if (isFlashed)
            {
                playerSprite.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0);
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
        else
        {
            playerSprite.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    /*
     * Hurts the player if the damage call is after the grace period
     * Player state machine goes to DamagedState
     * */
    public void takeDamage(int damage)
    {
        if (tGraced >= c.GRACE_PERIOD)
        {
            tGraced = 0;
            fsm.ChangeState<DamagedState>();
            playerHealth -= damage;
            if (playerHealth <= 0)
                killPlayer();
        }
    }

    /*
     * Kills the player: respawn, resets to FallState
     * and resets the grace timer, also ensures player is on default layer
     * */
    void killPlayer()
    {
        Debug.Log("MEMENTO MORI");
        respawnPlayer();

        fsm.ChangeState<DamagedState>();
        fsm.ChangeState<FallState>();

        tGraced = c.GRACE_PERIOD;
    }

    /*
     * Respawns player with PLAYER_MAX_HEALTH 
     * and the attached respawn location
     * */
    void respawnPlayer()
    {
        playerHealth = c.PLAYER_MAX_HEALTH;
        this.transform.position = respawnLocation.transform.position;
    }
}
