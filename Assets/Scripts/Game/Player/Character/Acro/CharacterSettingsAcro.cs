using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSettingsAcro : CharacterSettings
{
    public override Character GetSettingsFor()
    {
        Character c;

        //define movement settings

        c.WALKING_ACCELERATION = 25f;
        c.GRAVITY_ACCELERATION = 9f;
        c.WALK_MAX_SPEED = 2.4f;
        c.COYOTE_JUMP_TIME = 0.1f;
        c.BUFFER_JUMP_TIME = 0.1f;
        c.JUMP_VEL = 4.9f; //jump height
        c.MAX_FALL_SPEED = -4f;
        c.JUMP_STALL = 0.55f;

        c.GLIDE_SPEED = 0.2f;
        c.WALL_JUMP_DURATION = 0.02f;
        c.DASH_SPEED = 6f;
        c.DASH_DURATION = 0.4f;

        //define combat settings

        c.PLAYER_MAX_HEALTH = 20;
        c.GRACE_PERIOD = 5.0f;
        c.PLAYER_LAYER = 3;
        c.ENEMY_LAYER = 6;
        c.FLASH_PERIOD = 0.1f;

        return c;
    }
}

