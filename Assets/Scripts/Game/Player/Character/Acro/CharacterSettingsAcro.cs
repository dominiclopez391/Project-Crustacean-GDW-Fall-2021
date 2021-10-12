using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSettingsAcro : CharacterSettings
{
    public override Character GetSettingsFor()
    {
        Character c;

        c.WALKING_ACCELERATION = 25f;
        c.GRAVITY_ACCELERATION = 9f;
        c.WALK_MAX_SPEED = 2.4f;
        c.COYOTE_JUMP_TIME = 0.1f;
        c.JUMP_VEL = 4.9f; //jump height
        c.JUMP_STALL = 0.55f;

        return c;
    }
}

