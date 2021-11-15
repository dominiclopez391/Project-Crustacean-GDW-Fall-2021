using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public EnemySettings settings;
    Enemy e;
    int enemyHealth;
    public EnemyFSM fsm;
    float tGraced;

    //variables for managing flashing effect
    public GameObject enemySprite;
    float tFlash;
    bool isFlashed;

    // Start is called before the first frame update
    void Start()
    {
        e = settings.GetSettingsFor();
        enemyHealth = e.ENEMY_MAX_HEALTH;
        tGraced = e.GRACE_PERIOD;
        tFlash = 0.0f;
        isFlashed = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the grace period time, and flashing time
        if (tGraced < e.GRACE_PERIOD && fsm.GetState<EnemyStunState>())
        {
            //effects during the grace period
            tGraced += Time.deltaTime;
            if (tFlash < e.FLASH_PERIOD)
                tFlash += Time.deltaTime;
            else if (isFlashed)
            {
                enemySprite.GetComponent<SpriteRenderer>().color = new Color(255, 127, 0);
                isFlashed = false;
                tFlash = 0.0f;
            }
            else
            {
                enemySprite.GetComponent<SpriteRenderer>().color = Color.white;
                isFlashed = true;
                tFlash = 0.0f;
            }
        }
        //otherwise ensure player sprite is normal
        else
        {
            enemySprite.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    /*
     * Hurts the enemy if the damage call is after the grace period
     * Enemy state machine goes to EnemyStunState
     * */
    public void takeDamage(int damage)
    {
        if (tGraced >= e.GRACE_PERIOD)
        {
            tGraced = 0;
            fsm.ChangeState<EnemyStunState>();
            enemyHealth -= damage;
            if (enemyHealth <= 0)
                killEnemy();
        }
    }

    /*
     * Kills the enemy: deletion of entity from the world
     * */
    void killEnemy()
    {
        Debug.Log("C-C-C-COMBO KILL");
        Destroy(gameObject);
    }
}