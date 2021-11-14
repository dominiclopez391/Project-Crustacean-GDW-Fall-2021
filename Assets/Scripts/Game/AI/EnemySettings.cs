using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Enemy
{
    //movement settings
    public float WALKING_ACCELERATION;
    public float GRAVITY_ACCELERATION;
    public float WALK_MAX_SPEED;
    public float MAX_FALL_SPEED;
    public float COYOTE_JUMP_TIME;
    public float BUFFER_JUMP_TIME;
    public float JUMP_VEL; //jump height
    public float JUMP_STALL;

    public float GLIDE_SPEED;
    public float WALL_JUMP_DURATION;

    public float DASH_SPEED;
    public float DASH_DURATION;

    //AI settings
    public bool LEFT_FIRST;
    public bool IS_BOUNCER;
    public bool WATCHES_EDGES;

    //combat settings

    public int ENEMY_MAX_HEALTH;
    public float GRACE_PERIOD;
    public float FLASH_PERIOD;
};

public abstract class EnemySettings : MonoBehaviour
{
    public abstract Enemy GetSettingsFor();
}
