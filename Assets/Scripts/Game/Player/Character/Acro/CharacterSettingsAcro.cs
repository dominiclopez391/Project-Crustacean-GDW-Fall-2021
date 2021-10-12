using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSettingsAcro : CharacterSettings
{
    public override Character GetSettingsFor()
    {
        Character c;

        c.WALKING_ACCELERATION = 8f;
        c.GRAVITY_ACCELERATION = 14f;
        c.WALK_MAX_SPEED = 4f;
        c.COYOTE_JUMP_TIME = 0.1f;
        c.JUMP_VEL = 6f; //jump height
        c.JUMP_STALL = 0.65f;

        return c;
    }
}

