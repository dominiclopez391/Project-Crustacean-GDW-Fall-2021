using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySettingsPatroller : EnemySettings
{
    public override Enemy GetSettingsFor()
    {
        Enemy e;

        //define movement settings

        e.WALKING_ACCELERATION = 25f;
        e.GRAVITY_ACCELERATION = 9f;
        e.WALK_MAX_SPEED = 2.4f;
        e.COYOTE_JUMP_TIME = 0.1f;
        e.BUFFER_JUMP_TIME = 0.1f;
        e.JUMP_VEL = 4.9f; //jump height
        e.MAX_FALL_SPEED = -4f;
        e.JUMP_STALL = 0.55f;

        e.GLIDE_SPEED = 0.2f;
        e.WALL_JUMP_DURATION = 0.02f;
        e.DASH_SPEED = 6f;
        e.DASH_DURATION = 0.4f;

        //AI usage settings
        e.LEFT_FIRST = true;
        e.IS_BOUNCER = false;
        e.WATCHES_EDGES = true;

        //define combat settings

        e.ENEMY_MAX_HEALTH = 20;
        e.GRACE_PERIOD = 5.0f;
        e.FLASH_PERIOD = 0.1f;

        return e;
    }
}

