using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct Character
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

    //combat settings

    public int PLAYER_MAX_HEALTH;
    public float GRACE_PERIOD;
    public int PLAYER_LAYER;
    public int ENEMY_LAYER;
    public float FLASH_PERIOD;
};

public abstract class CharacterSettings : MonoBehaviour
{
    public abstract Character GetSettingsFor();
}
