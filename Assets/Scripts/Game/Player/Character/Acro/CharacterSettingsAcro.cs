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
        c.BUFFER_JUMP_TIME = 0.1f;
        c.JUMP_VEL = 4.9f; //jump height
<<<<<<< HEAD
        c.JUMP_STALL = 0.65f;
        c.MAX_FALL_SPEED = -4f;
=======
        c.JUMP_STALL = 0.55f;
>>>>>>> 0e681256a62746829f3a951e31416c56d65ce2f4

        return c;
    }
}

